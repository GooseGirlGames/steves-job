using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdrDialogue : DialogueTrigger {
    public Item _can_use_ddr;
    public Item _powered;
    public Item _notPowered;
    public Portal minigamePortal;
    public static DdrDialogue t = null;


    public override Dialogue GetActiveDialogue() {
        t = this;
        if(Inventory.Instance.HasItem(_can_use_ddr)){
        return new IntroDia();
        }
        if(Inventory.Instance.HasItem(_powered)){
            return new GgDia();
        }
        if(Inventory.Instance.HasItem(_notPowered)){
            return new LostGame();
        }
        return new NotPowered();
    }
    public class NotPowered : Dialogue {
        public NotPowered(){
            Say("...");
        }
    }

    public class IntroDia : Dialogue {
        public IntroDia(){
            Say("*beep beep* I am the BDR-Machine (Best Random Dance - Machine)");
            Say("Say..Do you want to start a game?")
            .Choice(
                new TextOption("okay")
                .IfChosen(new DialogueAction(() => {
                                t.minigamePortal.TriggerTeleport();
                            })))
            .Choice(
                new TextOption("later")
            );
        }
    }

    public class LostGame : Dialogue {
        public LostGame(){
            Say("try again?")
            .Choice(
                new TextOption("yeah")
                .IfChosen(new DialogueAction(() => {
                                t.minigamePortal.TriggerTeleport();
                            })))
            .Choice(
                new TextOption("not yet"));
        }
    }

    public class GgDia : Dialogue {
        public GgDia(){
            Say("GGWP!");
        }
    }
}
