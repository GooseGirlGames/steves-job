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
    public Image npcAvatar;
    public Canvas dialogueCanvas;
    public Transform textBox;
    public Sprite defaultBackground;
    public List<DialogueOptionUI> actionBoxes;
    public InteractionHintUI hintUI;
    public bool instantTrigger = false;

    //private Queue<OldSentence> sentences;
    //private Queue<UnityEvent> nextEvents;
    private DialogueElement root = null;
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
        dialogueCanvas.enabled = false;
        foreach (var actionBox in actionBoxes) {
            actionBox.gameObject.SetActive(false);
        }
    }

    public void StartDialogue(DialogueElement dialogueRoot) {

        root = dialogueRoot;

        ClearHint();

        /* TODO
        if (dialogue.background) {
            SetTextboxBackground(dialogue.background);
        } else {
            SetTextboxBackground(defaultBackground);
        } */

        DialogueManager.Log("Starting Dialogue " + root.ToString());
        
        dialogueCanvas.enabled = true;

        DisplayNextSentence();
    }

    public void SetInstantTrue(){
        instantTrigger = true;
    }

    public void DisplayNextSentence(Option chosenOption = null) {

        DialogueManager.Log("Displaying next sentence...");
        /* TODO?
        while (nextEvents.Count != 0) {
            nextEvents.Dequeue().Invoke();
        } */

        if (chosenOption != null) {
            if (root.current is Options) {
                ((Options) root).OptionChosen(chosenOption);
            } else {
                Debug.Log("chose option but we're not at Options!");
            }
        }

        if (!root.HasNext()) {
            DialogueEnded();
            return;
        }

        DialogueElement elem = root.Next();


        Debug.Log("base? " + elem.IsBase());
        if (!elem.ConditionsMet() || (elem.IsBase())) {
            DisplayNextSentence();
        }

        nameField.text = "TODO";
        if (elem is Sentence) {
            Sentence s = (Sentence) elem;
            textField.text = s.Text;
        }
        if (elem is Action) {
            Action a = (Action) elem;
            a.run();
            DisplayNextSentence();
        }
        // TODO npcAvatar.sprite = sentence.avatar;
        /* TODO
        if (sentence.background) {
            SetTextboxBackground(sentence.background);
        } */

        /* TODO!!
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
        */
        

        /* TODO?
        foreach (UnityEvent ev in sentence.onComplete) {
            nextEvents.Enqueue(ev);
        } */
    }

    private void DialogueEnded() {
        DialogueManager.Log("Dialog Ended");
        dialogueCanvas.enabled = false;
        root = null;
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
        return root != null;
    }

    public void HintAt(Dialogue dialogue) {
        hintUI.Hint(dialogue.hintPosition, DIALOGUE_KEY_STR);
    }
    public void ClearHint() {
        hintUI.ClearHint();
    }
 
}
