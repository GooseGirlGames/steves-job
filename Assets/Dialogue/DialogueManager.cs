using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    // FIXME hardcoding keybinds sucks an I am ashamed of myself
    public const KeyCode DIALOGUE_KEY = KeyCode.E;
    public static DialogueManager Instance;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI textField;
    public Canvas dialogueCanvas;
    public Transform textBox;
    public Sprite defaultBackground;
    public List<DialogueOptionUI> actionBoxes;
    public bool instantTrigger = false;

    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;
    private Dialogue activeDialogue = null;
    private Camera cam;
    private bool canBeAdvancedByKeypress = true;  // false iff an action must be chosen to continue

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    void Start() {
        sentences = new Queue<Sentence>();
        nextEvents = new Queue<UnityEvent>();
        dialogueCanvas.enabled = false;
        foreach (var actionBox in actionBoxes) {
            actionBox.gameObject.SetActive(false);
        }
    }

    public void StartDialogue(Dialogue dialogue) {

        activeDialogue = dialogue;
        if (dialogue.background) {
            SetTextboxBackground(dialogue.background);
        } else {
            SetTextboxBackground(defaultBackground);
        }

        cam = GameObject.FindObjectOfType<Camera>();
        PositionDiabox();

        Debug.Log("Starting Dialogue");

        dialogueCanvas.enabled = true;

        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void setInstantTrue(){
        instantTrigger = true;
    }

    public void DisplayNextSentence(DialogueOption chosenOption = null) {
        while (nextEvents.Count != 0) {
            nextEvents.Dequeue().Invoke();
        }

        if (chosenOption != null) {
            foreach(UnityEvent ev in chosenOption.onChose) {
                ev.Invoke();
            }
        }

        if (sentences.Count == 0) {
            DialogueEnded();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        nameField.text = sentence.name;
        textField.text = sentence.text;

        if (sentence.options.Count != 0) {
            canBeAdvancedByKeypress = false;

            foreach (var actionBox in actionBoxes)
                actionBox.gameObject.SetActive(false);

            int n = Mathf.Min(sentence.options.Count, actionBoxes.Count);
            for (int i = 0; i < n; ++i) {
                
                DialogueOptionUI actionBox = actionBoxes[i];
                actionBox.gameObject.SetActive(true);
                DialogueOption option = sentence.options[i];

                actionBox.option = option;

                TextMeshProUGUI text = actionBox.text;
                Image image = actionBox.image;

                if (option.dialogueOptionType == DialogueOption.DialogueOptionType.ItemAction) {
                    text.text = option.text + ' ' + option.item.name;
                    image.sprite = option.item.icon;
                    if (Inventory.Instance)
                        actionBox.button.interactable = Inventory.Instance.HasItem(option.item);
                } else if (option.dialogueOptionType == DialogueOption.DialogueOptionType.TextAction) {
                    text.text = option.text;
                    image.gameObject.SetActive(false);
                    actionBox.button.interactable = true;
                }
    
            }
        } else {
            foreach (var actionBox in actionBoxes)
                actionBox.gameObject.SetActive(false);
            canBeAdvancedByKeypress = true;
        }
        

        foreach (UnityEvent ev in sentence.onComplete) {
            nextEvents.Enqueue(ev);
        }
    }

    private void DialogueEnded() {
        dialogueCanvas.enabled = false;
        activeDialogue = null;
    }

    private void OnGUI() {
        if (IsDialogueActive())
            PositionDiabox();
    }
    private void PositionDiabox() {
        
        Vector3 diaboxPositionScreen = cam.WorldToScreenPoint(activeDialogue.diaboxPosition.position);
        // i think this sucks, but it works
        float scale = dialogueCanvas.scaleFactor;
        Vector2 diaboxRect = scale * textBox.GetComponentInChildren<RectTransform>().rect.size;

        Vector3 pos = diaboxPositionScreen;
        
        pos.y -= -diaboxRect.y/2;
        
        float marginX = 25.0f;
        float screenWidth = cam.pixelWidth;

        float minX = marginX + diaboxRect.x/2;
        float maxX = screenWidth - marginX - diaboxRect.x/2;
        
        pos.x = Mathf.Clamp(pos.x + diaboxRect.x/5, minX, maxX);

        textBox.position = pos;
    }

    private void SetTextboxBackground(Sprite sprite) {
        Image bg = textBox.GetComponentInChildren<Image>();
        bg.sprite = sprite;
    }

    private void Update() {
        if (canBeAdvancedByKeypress && Input.GetKeyDown(DIALOGUE_KEY)) {
            DisplayNextSentence();
        }
    }

    public bool IsDialogueActive() {
        return activeDialogue != null;
    }
 
}
