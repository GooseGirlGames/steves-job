using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleExtendedDialogueTrigger : DialogueTrigger
{
    public Dialogue bye;
    private bool saidHello = false;
    public override Dialogue GetActiveDialogue() {
        if (!saidHello) {
            saidHello = true;
            return defaultDialogue;
        } else {
            return bye;
        }
    }
}
