using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAction {
    public delegate void RunFunc();
    public RunFunc Run;
    public DialogueAction(RunFunc run) {
        Run = run;
    }
}

/*
public class TriggerDialogueAction : DialogueAction  {
    public TriggerDialogueAction() : base(() => {}) {}

    public TriggerDialogueAction<T>() : base(() => {
            Dialogue d = new Dialogue();
            DialogueManager.Instance.StartDialogue(dialogue);
    }) {} 
}*/


public class TriggerDialogueAction<T> : DialogueAction where T: Dialogue, new() {
    public TriggerDialogueAction() : base(() => {
            Debug.Log("building new dialogue");
            Dialogue d = (Dialogue) new T();
            DialogueManager.Instance.StartDialogue(d);
    }) {
        Debug.Log("Building new triggerdialogueaction");
    }
    /*{
        Run = () => {
            Dialogue d = (Dialogue) new T();
            DialogueManager.Instance.StartDialogue(d);
        };
    }*/
}