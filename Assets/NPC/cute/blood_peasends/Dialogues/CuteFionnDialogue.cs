using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteFionnDialogue : DialogueTrigger {
    public static CuteFionnDialogue t;
    public Item joined;
    public Item goose;
    public Item bloodygoose;
    public override Dialogue GetActiveDialogue() {
        t = this;
        if (!Inventory.Instance.HasItem(joined)) {
            return new WannaJoin();
        }
        return new ShowGoose();
    }

    public class WannaJoin : Dialogue {
        public WannaJoin() {
            Say("Hello! We are the cult of GOOSE.");
            Say(
                "Interested in joining us?\n "
                + "We might not be the flashiest cult around, "
                + "but if you decide to join, you will get this "
                + "lovely GOOSE as a limited-time new member reward!"
            );
            Say("Time is limited though!")
            .Choice(
                new TextOption("Join cult of GOOSE")
                .IfChosen(GiveItem(t.joined))
                .IfChosen(new TriggerDialogueAction<Joined>())
            )
            .Choice(
                new TextOption("Maybe later")
            );
        }
    }

    public class Joined : Dialogue {
        public Joined() {
            Say("Welcome to the cult of GOOSE.");
            
            Say("Here's your reward. Take good care of her.")
            .Do(GiveItem(t.goose));
        }
    }

    public class ShowGoose : Dialogue {
        public ShowGoose() {
            Say("Got something to show us?")
            .Choice(
                new ItemOption(t.goose)
                .IfChosen(new TriggerDialogueAction<NiceGoose>())
            )
            .Choice(
                new ItemOption(t.bloodygoose)
                .IfChosen(new TriggerDialogueAction<PoorGoose>())
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<OtherItem>())
            )
            .Choice(new TextOption("Nope"));
        }
    }

    public class NiceGoose : Dialogue {
        public NiceGoose() {
            Say("What a beauty!");
        }
    }

    public class PoorGoose : Dialogue {
        public PoorGoose() {
            Say(
                "AAAAAAAAAAAhhhhhh. What have you "
                + "done to that poor little thing?"
            );
        }
    }

    public class OtherItem : Dialogue {
        public OtherItem() {
            string item = DialogueManager.Instance.currentItem.name;
            Say("Umm, nice " + item + ", I guess?");
        }
    }

}
