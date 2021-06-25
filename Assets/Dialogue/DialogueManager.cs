// uncomment to enable debug logging for the dialogue system
#define DEBUG_DIALOGUE_SYSTEM

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
    public Image npcAvatar;
    public Canvas dialogueCanvas;
    public Transform textBox;
    //public Sprite defaultBackground;
    public List<DialogueOptionUI> actionBoxes;
    public InteractionHintUI hintUI;
    public bool instantTrigger = false;

    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;
    private Dialogue activeDialogue = null;
    private Sentence currentSentence = null;
    private DialogueTrigger currentTrigger = null;  // current trigger
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

    public void StartDialogue(Dialogue dialogue, bool clearCurrent = false, DialogueTrigger trigger = null) {

        if (trigger != null) {
            currentTrigger = trigger;
        }

        bool otherDialogueWasActive = IsDialogueActive();

        //lastKeyPress = Time.fixedTime + 0.1f;

        ClearHint();
        currentSentence = null;

        activeDialogue = dialogue;
        if (currentTrigger && currentTrigger.background) {
            SetTextboxBackground(currentTrigger.background);
        } else {
            SetTextboxBackground(defaultBackground);
        }

        DialogueManager.Log("Starting Dialogue");
        
        dialogueCanvas.enabled = true;

        if (otherDialogueWasActive && clearCurrent) {
            sentences.Clear();
        }
        if (otherDialogueWasActive && !clearCurrent) {
            List<Sentence> newSentences = new List<Sentence>(dialogue.sentences);
            newSentences.AddRange(sentences);
            sentences = new Queue<Sentence>(newSentences);
        } else {
            foreach (Sentence sentence in dialogue.sentences) {
                sentences.Enqueue(sentence);
            }
        }

        // E.g. for triggering via DialogueAction, we only want to enqueue the sentences.
        // Otherwise, pressing E will skip the first sentence.
        // So this basically debounces the keypress.
        if (!otherDialogueWasActive)
            DisplayNextSentence();
    }

    public void SetInstantTrue(){
        instantTrigger = true;
    }

    // Clicking an item while in a dialogue triggers DisplayNextSentence,
    // with the DialogueOption holding whatever item was seletced.
    // The item may or may not trigger a specific or useful action/respoonse.
    public void DisplayNextSentence(DialogueOption chosenOption = null) {

        DialogueManager.Log("Displaying next sentence...");
        //while (nextEvents.Count != 0) {
        //    nextEvents.Dequeue().Invoke();
        //}

        if (chosenOption != null) {
            foreach (DialogueAction action in chosenOption.onChose) {
                action.Run();
            }
        }

        // Trigger Actions *after* sentence is said, and E has been pressed:
        if (currentSentence != null)
            currentSentence.Act(sentenceStillOnScreen: false);

        if (sentences.Count == 0) {
            DialogueEnded();
            return;
        }

        Sentence sentence = sentences.Dequeue();

        if (!sentence.ConditionsFulfilled()) {
            Debug.Log("Skipping " + sentence.text);
            DisplayNextSentence();
            return;
        }

        currentSentence = sentence;

        // Placing this after setting `currentSentence`, so actions will still get exectued
        if (sentence is EmptySentence) {
            DisplayNextSentence();
            return;
        }

        DialogueManager.Log("Dialogue text: '" + sentence.text + "'");
        
        textField.text = sentence.text;
        
        if (currentTrigger != null) {
            nameField.text = currentTrigger.name;
            // TODO npcAvatar.sprite = currentTrigger.avatar;
        }
        

        List<DialogueOption> availableOptions = new List<DialogueOption>();
        foreach (DialogueOption option in sentence.options) {
            if (DialogueCondition.ConditionsFullFilled(option.conditions)) {
                availableOptions.Add(option);
            }
        }

        if (availableOptions.Count != 0) {
            canBeAdvancedByKeypress = false;

            foreach (var actionBox in actionBoxes)
                actionBox.gameObject.SetActive(false);

            int n = Mathf.Min(availableOptions.Count, actionBoxes.Count);
            for (int i = 0; i < n; ++i) {
                
                DialogueOptionUI actionBox = actionBoxes[i];
                actionBox.gameObject.SetActive(true);
                DialogueOption option = availableOptions[i];

                actionBox.option = option;

                TextMeshProUGUI text = actionBox.text;
                Image image = actionBox.image;

                if (option is ItemOption) {
                    ItemOption itemOption = (ItemOption) option;
                    text.text = itemOption.item.name;
                    image.sprite = itemOption.item.icon;
                } else if (option is TextOption) {
                    TextOption textOption = (TextOption) option;
                    text.text = textOption.text;
                    image.gameObject.SetActive(false);
                    actionBox.button.interactable = true;
                }
    
            }
        } else {
            foreach (var actionBox in actionBoxes)
                actionBox.gameObject.SetActive(false);
            canBeAdvancedByKeypress = true;
        }

        if (currentSentence != null)
            currentSentence.Act(sentenceStillOnScreen: true);

    }

    public void EndDialogue() {
        sentences.Clear();
        DialogueEnded();
    }

    private void DialogueEnded() {
        DialogueManager.Log("Dialog Ended");
        dialogueCanvas.enabled = false;
        activeDialogue = null;
        currentSentence = null;
        currentTrigger = null;
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
            //DialogueManager.Log("Too fast " + Time.fixedTime + ", " + lastKeyPress);
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

    public void HintAt(DialogueTrigger trigger) {
        Debug.Log("Hinting at dialogue by " + trigger.name);
        hintUI.Hint(trigger.hintPosition, DIALOGUE_KEY_STR);
    }
    public void ClearHint() {
        hintUI.ClearHint();
    }
 
}
