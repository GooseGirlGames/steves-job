using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

 */
public class JimDialogue : DialogueTrigger{
    public Item _jim_horror_finished;
    public Item _jim_cute_finished;
    public Item _minigamewon;
    public static JimDialogue t;

    public override Dialogue GetActiveDialogue() {
        JimDialogue.t = this;
        return new NoneExist();
    }

    public class NoneExist : Dialogue{
        public NoneExist(){
            Say("...");
        }
    }
}
