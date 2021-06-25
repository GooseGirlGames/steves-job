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

public class TriggerDialogueAction<T> : DialogueAction where T: Dialogue, new() {
    public TriggerDialogueAction(bool exitCurrent = false) : base(() => {
            Debug.Log("building new dialogue");
            // TODO: Maybe move this into a helper method of Dialogue?  could be useful elsewhere.
            Dialogue d = (Dialogue) new T();
            DialogueManager.Instance.StartDialogue(d, exitCurrent);
    }) {
        Debug.Log("Building new triggerdialogueaction");
    }
}