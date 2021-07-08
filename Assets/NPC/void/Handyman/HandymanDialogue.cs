using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandymanDialogue : DialogueTrigger{
    public Item _jim_horror_finished;
    public Item _jim_cute_finished;
    public Item _minigamewon;
    public static HandymanDialogue t;

    public override Dialogue GetActiveDialogue() {
        HandymanDialogue.t = this;
        if(Inventory.Instance.HasItem(_jim_cute_finished)&&Inventory.Instance.HasItem(_jim_horror_finished)){
            return new RestoredDialogue();
        }
        else if(Inventory.Instance.HasItem(_jim_cute_finished)||Inventory.Instance.HasItem(_jim_horror_finished)){
            return new HalfExist();
        }
        return new NoneExist();
    }
    public class NoneExist : Dialogue{
        public NoneExist(){
            Say("...");
        }
    }
    public class HalfExist : Dialogue{
        public HalfExist(){
            Say("AAAAAAAAAAAAAAAAAAAAAAAA, why is this happening???");
        }
    }
    public class RestoredDialogue : Dialogue{
        public RestoredDialogue(){
            Say("...");
        }
    }
}
