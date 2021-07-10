using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteFionnDialogue : DialogueTrigger {
    public static CuteFionnDialogue t;
    public Item joined;
    public Item later;
    public Item goose;
    public Item goosebloody;
    public Item goosebow;
    public Item goosebloodybow;
    public Item magpie;
    public Item grease;
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
                new ItemOption(t.goosebow)
                .IfChosen(new TriggerDialogueAction<NiceGooseBow>())
            )
            .Choice(
                new ItemOption(t.goosebloody)
                .IfChosen(new TriggerDialogueAction<NiceGooseBloody>())
            )
            .Choice(
                new ItemOption(t.goosebloodybow)
                .IfChosen(new TriggerDialogueAction<NiceGooseBloodyBow>())
            )
            .Choice(
                new ItemOption(t.grease)
                .IfChosen(new TriggerDialogueAction<Grease>())
            )
            .Choice(
                new ItemOption(t.magpie)
                .IfChosen(new TriggerDialogueAction<Magpie>())
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
    public class Magpie : Dialogue {
        public Magpie() {
            Say("Haha, that's a funny looking goose you got there.");

            Say("I'm sure your actual Goose is doing just as fine as that Magpie though.")
            .If(HasItem(t.grease));
            Say("...right?")
            .If(HasItem(t.grease));
        }
    }
    public class NiceGooseBow : Dialogue {
        public NiceGooseBow() {
            Say("What a beauty! She's so cute :3");
        }
    }
    public class NiceGooseBloody : Dialogue {
        public NiceGooseBloody() {
            Say("Oh no, what happened to our little honker?");
            Say("Maybe try to get her cleaned up?");
            Say("Actually, please do. I'll call Goose Protective Services on you if you don't.");
        }
    }
    public class NiceGooseBloodyBow : Dialogue {
        public NiceGooseBloodyBow() {
            Say("Ummmmm...");
            Say("I mean that's a cute bow.. but... how did we end up here?");
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
