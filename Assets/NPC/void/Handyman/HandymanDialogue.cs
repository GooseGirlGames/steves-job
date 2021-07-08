using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandymanDialogue : DialogueTrigger{
    public Item _jim_horror_finished;
    public Item _jim_cute_finished;
    public Item _minigamewon;
    public static HandymanDialogue t;

    public override Dialogue GetActiveDialogue() {
        return new NoneExist();
    }
    public class NoneExist : Dialogue{
        public NoneExist(){
            Say("...");
        }
    }
}
