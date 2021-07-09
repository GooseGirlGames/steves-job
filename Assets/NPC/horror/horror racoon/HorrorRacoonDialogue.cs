using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorRacoonDialogue : DialogueTrigger
{
    public Item bloodymary;
    public Item _horror_racoon;
    public Item bloodbucket;
    public Item emptybucket;
    public Item grease;
    public Item goose;

    public static HorrorRacoonDialogue h;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

        if (! Inventory.Instance.HasItem(_horror_racoon)) {
            return new HorrorRacoonFinishedDilaogue();
        }
        return new HorrorRacoonDefaultDialogue();
    }

    public class HorrorRacoonFinishedDilaogue : Dialogue {
        public HorrorRacoonFinishedDilaogue() {
            Say("zZzzZzzzZZZzzzZZ");
            Say("Hm? I am taking an after-food nap, don't disturb me");
            Say("zZZzzZZZzzzzzzZzZ");
        }
    }

    public class HorrorRacoonHi : Dialogue {
        public HorrorRacoonHi() {
            Say("Hi there");
            Say("I am a bit leached our beacuse I forgot to stay hydrated")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonDefaultDialogue : Dialogue {
        public HorrorRacoonDefaultDialogue() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

            Say("Have you found something I can drink?")
            .Choice(new TextOption("I am afraid I haven't")
                .IfChosen(new TriggerDialogueAction<HorrorRacoonBye>()))
            .Choice(new ItemOption(h.bloodbucket)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonBucket>()))
            .Choice(new ItemOption(h.goose)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.grease)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGrease>()))
            .Choice(new ItemOption(h.bloodymary)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonDrink>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<HorrorRacoonItem>()));
        }
    }

    public class HorrorRacoonBye : Dialogue {
        public HorrorRacoonBye() {
            Say("If you come across something to drink be sure to gie it to me :)");
        }
    }

    public class HorrorRacoonBucket : Dialogue {
        public HorrorRacoonBucket() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

            Say("Oh that doesn't look half bad")
            .DoAfter(RemoveItem(h.bloodbucket))
            .DoAfter(GiveItem(h.emptybucket));
            Say("*glug* *glug* *glug*");
            Say("Aaaaaah");
            Say("It isn't half bad but I'd like something more refined")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonGoose : Dialogue {
        public HorrorRacoonGoose() {
            Say("That's very nice of you, however recently I've started eating only free flying birds");
            Say("I like the challenge and now eating birds just given to me feels wrong")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonGrease : Dialogue {
        public HorrorRacoonGrease() {
            Say("I don't know, I am not a machine");
            Say("Think I'll pass...")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonDrink : Dialogue {
        public HorrorRacoonDrink() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

            Say("What's that?");
            Say("It smells amazing!");
            Say("*slurp* *slurp*");
            Say("delicious!");
            Say("I feel energized again!");
            Say("Thanks!")
            .DoAfter(RemoveItem(h.bloodymary))
            .DoAfter(GiveItem(h._horror_racoon));
        }
    }

    public class HorrorRacoonItem : Dialogue {
        public HorrorRacoonItem() {
            Say("Hm, looks interesting but it can't quench my thirst");
        }
    }
}
