// uncomment to enable debug logging for the dialogue system
// #define DEBUG_DIALOGUE_SYSTEM

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
    public const string DIALOGUE_KEY_STR = "E";
    public static DialogueManager Instance;

    public TextMeshProUGUI nameField;
    public TextMeshProUGUI textField;
    public Canvas dialogueCanvas;
    public Transform textBox;
    public Sprite defaultBackground;
    public List<DialogueOptionUI> actionBoxes;
    public InteractionHintUI hintUI;
    public bool instantTrigger = false;

    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;
    private Dialogue activeDialogue = null;
    private Camera cam;
    private bool canBeAdvancedByKeypress = true;  // false iff an action must be chosen to continue

    public float lastKeyPress = -1.0f;
    public const float KEY_PRESS_TIME_DELTA = 0.3f;  // seconds

    public static void Log(string msg) {
        #if DEBUG_DIALOGUE_SYSTEM
            Debug.Log(msg);
        #endif
    }

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

        ClearHint();

        activeDialogue = dialogue;
        if (dialogue.background) {
            SetTextboxBackground(dialogue.background);
        } else {
            SetTextboxBackground(defaultBackground);
        }

        cam = GameObject.FindObjectOfType<Camera>();
        PositionDiabox();

        DialogueManager.Log("Starting Dialogue");
        
        dialogueCanvas.enabled = true;

        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void SetInstantTrue(){
        instantTrigger = true;
    }

    public void DisplayNextSentence(DialogueOption chosenOption = null) {
        DialogueManager.Log("Displaying next sentence...");
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
        DialogueManager.Log(sentence.name + " says: '" + sentence.text + "'");
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
        DialogueManager.Log("Dialog Ended");
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
        if (!IsDialogueActive()) {
            return;
        }
        
        if (Time.fixedTime - lastKeyPress < KEY_PRESS_TIME_DELTA) {
            DialogueManager.Log("Too fast " + Time.fixedTime + ", " + lastKeyPress);
            return;
        }
        if (canBeAdvancedByKeypress && Input.GetKeyDown(DIALOGUE_KEY)) {
            lastKeyPress = Time.fixedTime;
            DisplayNextSentence();
        }
    }

    public bool IsDialogueActive() {
        return activeDialogue != null;
    }

    public void HintAt(Dialogue dialogue) {
        hintUI.Hint(dialogue.diaboxPosition, DIALOGUE_KEY_STR);
    }
    public void ClearHint() {
        hintUI.ClearHint();
    }
 
}
