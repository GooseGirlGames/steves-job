using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Text nameField;
    public Text textField;
    public Canvas dialogueCanvas;
    private Queue<Sentence> sentences;
    private Queue<UnityEvent> nextEvents;

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

        dialogueCanvas.enabled = true;

        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        while (nextEvents.Count != 0) {
            nextEvents.Dequeue().Invoke();
        }

        if (sentences.Count == 0) {
            dialogueCanvas.enabled = false;
            return;
        }
        Sentence sentence = sentences.Dequeue();
        nameField.text = sentence.name;
        textField.text = sentence.text;

        foreach (UnityEvent ev in sentence.onComplete) {
            nextEvents.Enqueue(ev);
        }
    }

    private void Update() {
        // FIXME hardcoding keybinds sucks an I am ashamed of myself
        if (Input.GetKeyDown(KeyCode.E)) {
            DisplayNextSentence();
        }
    }
 
}
