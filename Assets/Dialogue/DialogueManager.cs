// uncomment to enable debug logging for the dialogue system
#define DEBUG_DIALOGUE_SYSTEM

using System;
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
    public Sprite defaultBackground;
    public Sprite defaultAvatar;
    public List<Animator> uiAnimators;

    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;
    private Dialogue activeDialogue = null;
    private Sentence currentSentence = null;
    private DialogueTrigger currentTrigger = null;  // current trigger
    private bool canBeAdvancedByKeypress = true;  // false iff an action must be chosen to continue

    public float lastKeyPress = -1.0f;
    public const float KEY_PRESS_TIME_DELTA = 0.3f;  // seconds
    public const float NEW_DIALOGUE_START_COOLDOWN = 0.0f;  // prevents option selection immediately re-triggering dialogue
    private const string DIALOGUE_LOCK_TAG = "DialogueManager";

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

        stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
        steve.Lock(DIALOGUE_LOCK_TAG);

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

        foreach (Animator uiAnimator in uiAnimators) {
            uiAnimator.SetInteger("World", (int) currentTrigger.world);
        }

        InventoryCanvasSlots.Instance.Show();

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
    public void DisplayNextSentence(DialogueOption chosenOption = null, Item item = null) {

        /* Happens when an inventory item button is pressed outside of a dialogue */
        if (!IsDialogueActive()) return;

        // When E is pressed to chose an option, the keypress time is not
        // caught in DialogueManager's Update(), because DisplayNextSentence is
        // called immedialtely from the button.
        // This prevents re-triggering a dialogue that was exited by choosing an option.
        // See #67 fore more info.
        if (chosenOption != null || item != null) {
            lastKeyPress = Time.fixedTime;
        }

        if (item != null) {
/*             InventoryCanvasSlots.Instance.SetActionBoxVisibility(true);
            Debug.Log("DialogueMan is hiding lore");
            InventoryCanvasSlots.Instance.HideItemLoreBox(); */
            Debug.Log("We've got an item!  It's " + item.name);

            foreach (DialogueOption opt in currentSentence.options) {
                if (opt is ItemOption) {
                    ItemOption itemOption = (ItemOption) opt;
                    if (item == itemOption.item) {
                        Debug.Log("Found option for item");
                        chosenOption = opt;
                    }
                }
            }
            if (chosenOption == null) {
                Debug.Log("No option for item, searching OtherItemOption");
                // Player has chosen an item for which no option is attachted to the sentence.
                foreach (DialogueOption option in currentSentence.options) {
                    if (option is OtherItemOption) {
                        Debug.Log("OtherItemOption found");
                        chosenOption = option;
                    }
                }
            }
            if (chosenOption == null) {
                Debug.Log("Still no item, irgh");
                return;
            }
        }

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
        
        textField.text = Uwu.OptionalUwufy(sentence.text);
        
        if (currentTrigger != null) {
            nameField.text = Uwu.OptionalUwufy(currentTrigger.name);
            if (currentTrigger.avatar)
                npcAvatar.sprite = currentTrigger.avatar;
            else
                npcAvatar.sprite = defaultAvatar;
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

            List<DialogueOption> shownOptions = new List<DialogueOption>();
            foreach (DialogueOption availableOption in availableOptions) {
                if (availableOption is TextOption) {
                    shownOptions.Add(availableOption);
                }
            }

            int n = Mathf.Min(shownOptions.Count, actionBoxes.Count);
            for (int i = 0; i < n; ++i) {
                
                DialogueOptionUI actionBox = actionBoxes[i];
                actionBox.gameObject.SetActive(true);
                DialogueOption option = shownOptions[i];

                actionBox.option = option;

                TextMeshProUGUI text = actionBox.text;
                Image image = actionBox.image;

                if (option is ItemOption) {
                    ItemOption itemOption = (ItemOption) option;
                    text.text = Uwu.OptionalUwufy(itemOption.item.name);
                    image.sprite = itemOption.item.icon;
                } else if (option is TextOption) {
                    TextOption textOption = (TextOption) option;
                    text.text = Uwu.OptionalUwufy(textOption.text);
                    image.gameObject.SetActive(false);
                    actionBox.button.interactable = true;
                }
                if (i == 0) {
                    StartCoroutine( UIUtility.SelectButtonLater(actionBox.button));
                }
            }
            foreach (Animator uiAnimator in uiAnimators) {
                Debug.Log("uiAnimator " + uiAnimator.name
                        + " active" + uiAnimator.gameObject.activeSelf + " set to"
                        + (int) currentTrigger.world);
                uiAnimator.SetInteger("World", (int) currentTrigger.world);
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
        lastKeyPress += NEW_DIALOGUE_START_COOLDOWN;
        DialogueManager.Log("Dialog Ended");
        dialogueCanvas.enabled = false;
        //canBeAdvancedByKeypress = true;
        activeDialogue = null;
        currentSentence = null;
        currentTrigger = null;
        stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
        steve.Unlock(DIALOGUE_LOCK_TAG);
        InventoryCanvasSlots.Instance.Hide();
    }

    private void SetTextboxBackground(Sprite sprite) {
        Image bg = textBox.GetComponentInChildren<Image>();
        bg.sprite = sprite;
    }

    private void Update() {
        if (!IsDialogueActive()) {
            // Set anyway to avoid re-triggering a dialogue when a dialogue is exited 
            // because an option was chosen.  Also see #67.
            // TODO if (Input.GetKeyDown(DIALOGUE_KEY)) lastKeyPress = Time.fixedTime;
            return;
        }
        
        bool tooFast = (Time.fixedTime - lastKeyPress) < KEY_PRESS_TIME_DELTA;
        
        if (Input.GetKeyDown(DIALOGUE_KEY)) {
            DialogueManager.Log("E Pressed!");
            lastKeyPress = Time.fixedTime;
            if (!tooFast && canBeAdvancedByKeypress) {
                DisplayNextSentence();
            }
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
