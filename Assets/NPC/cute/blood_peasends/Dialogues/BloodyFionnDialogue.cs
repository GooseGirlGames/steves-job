using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyFionnDialogue : DialogueTrigger {
    public static BloodyFionnDialogue t;
    public Item joined;
    public Item later;
    public Item grease;
    public Item later_bloodsoaked;
    public Item goose;
    public Item goosebloody;
    public Item goosebow;
    public Item goosebloodybow;
    public Item magpie;
    public override Dialogue GetActiveDialogue() {
        t = this;
        if (!Inventory.Instance.HasItem(joined)) {
            if (Inventory.Instance.HasItem(later_bloodsoaked)) {
                return new WannaJoin();
            } else if (Inventory.Instance.HasItem(later)) {
                return new HelloAgain();
            }
            return new Hello();
        }
        return new ShowGoose();
    }

    public class HelloAgain : Dialogue {
        public HelloAgain() {
            Say("Sorry about the mess, but we are still in fact the cult of GOOSE.")
            .DoAfter(new TriggerDialogueAction<WannaJoin>());
        }
    }

    public class Hello : Dialogue {
        public Hello() {
            Say("Hello! Sorry about the mess, but we are the cult of GOOSE.");
            Say(
                "Interested in joining us? \n"
                + "We might not be the cutest cult "
                + "(anymore, we'll get this whole situation taken care of), "
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
                .IfChosen(GiveItem(t.later_bloodsoaked))
            );
        }
    }
    public class WannaJoin : Dialogue {
        public WannaJoin() {
            Say("So, still want to join us?")
            .Choice(
                new TextOption("Join cult of GOOSE")
                .IfChosen(new TriggerDialogueAction<Joined>())
            )
            .Choice(
                new TextOption("Maybe later")
                .IfChosen(GiveItem(t.later_bloodsoaked))
            );
        }
    }

    public class Joined : Dialogue {
        public Joined() {
            Say("Welcome to the cult of GOOSE.");

            Say("Here's your reward. Take better care of her than we did.")
            .Do(GiveItem(t.joined))
            .Do(GiveItem(t.goosebloody));
        }
    }

    public class ShowGoose : Dialogue {
        public ShowGoose() {
            Say("Got something to show us?")
            .Choice(
                new ItemOption(t.goose)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.NiceGoose>())
            )
            .Choice(
                new ItemOption(t.goosebow)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.NiceGooseBow>())
            )
            .Choice(
                new ItemOption(t.goosebloody)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.NiceGooseBloody>())
            )
            .Choice(
                new ItemOption(t.goosebloodybow)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.NiceGooseBloodyBow>())
            )
            .Choice(
                new ItemOption(t.grease)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.Grease>())
            )
            .Choice(
                new ItemOption(t.magpie)
                .IfChosen(new TriggerDialogueAction<CuteFionnDialogue.Magpie>())
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<OtherItem>())
            )
            .Choice(new TextOption("Nope"));
        }
    }


    public class OtherItem : Dialogue {
        public OtherItem() {
            string item = DialogueManager.Instance.currentItem.name;
            Say("Umm, nice " + item + ", I guess?");
        }
    }

}
