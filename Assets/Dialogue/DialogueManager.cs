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

    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;
    private Dialogue activeDialogue = null;
    private Camera cam;

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

    private void DisplayNextSentence() {
        while (nextEvents.Count != 0) {
            nextEvents.Dequeue().Invoke();
        }

        if (sentences.Count == 0) {
            DialogueEnded();
            return;
        }
        Sentence sentence = sentences.Dequeue();
        nameField.text = sentence.name;
        textField.text = sentence.text;

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

        Rect diaboxRect = textBox.GetComponentInChildren<RectTransform>().rect;

        Vector3 pos = diaboxPositionScreen - new Vector3(0, -diaboxRect.height/2, 0);
        
        float marginX = 0.0f;
        float screenWidth = cam.pixelWidth;

        float minX = marginX + diaboxRect.width/2;
        float maxX = screenWidth - marginX - diaboxRect.width/2;
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        textBox.position = pos;
    }

    private void SetTextboxBackground(Sprite sprite) {
        Image bg = textBox.GetComponentInChildren<Image>();
        bg.sprite = sprite;
    }

    private void Update() {
        if (Input.GetKeyDown(DIALOGUE_KEY)) {
            DisplayNextSentence();
        }
    }

    public bool IsDialogueActive() {
        return activeDialogue != null;
    }
 
}
