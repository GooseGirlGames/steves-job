using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteFionnDialogue : DialogueTrigger {
    public static CuteFionnDialogue t;
    public Item joined;
    public Item later;
    public Item goose;
    public Item grease;
    public Item bloodygoose;
    public override Dialogue GetActiveDialogue() {
        t = this;
        if (!Inventory.Instance.HasItem(joined)) {
            if (Inventory.Instance.HasItem(later)) {
                return new WannaJoin();
            }
            return new Hello();
        }
        return new ShowGoose();
    }

    public class Hello : Dialogue {
        public Hello() {
            Say("Hello! We are the cult of GOOSE.");
            Say(
                "Interested in joining us? \n"
                + "We might not be the flashiest cult around, "
                + "but if you decide to join, you will get this "
                + "lovely GOOSE as a limited-time new member reward!"
            );
            Say("Time is limited though!")
            .Choice(
                new TextOption("Join cult of GOOSE")
                .IfChosen(new TriggerDialogueAction<Joined>())
            )
            .Choice(
                new TextOption("Maybe later")
                .IfChosen(GiveItem(t.later))
            );
        }
    }
    public class WannaJoin : Dialogue {
        public WannaJoin() {
            Say("So, still want to join us?")
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
            .Do(GiveItem(t.joined))
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
                new ItemOption(t.grease)
                .IfChosen(new TriggerDialogueAction<Grease>())
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

    public class Grease: Dialogue {
        public Grease() {
            Say("Is that... Oh my god...");
        }
    }

    public class OtherItem : Dialogue {
        public OtherItem() {
            string item = DialogueManager.Instance.currentItem.name;
            Say("Umm, nice " + item + ", I guess?");
        }
    }

}