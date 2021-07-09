using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveEHorrorDialogue : DialogueTrigger
{
    public Item babelfisch;
    public Item horrorcoin;
    public Item _given_horrorcoin;
    public Item _setve_horror_backstory;
    public Item void_bar;
    public Item cutecoin;
    public Item startcoin;


    public static SteveEHorrorDialogue s;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        SteveEHorrorDialogue.s = this;

        if (! Inventory.Instance.HasItem(_setve_horror_backstory)) {
            return new StvHorrorBackstoryDialogue();
        }
        else if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            return new StvHorrorHappyDialogue();
        }
        return new StvHorrorDefaultDialogue();
    }

    public class StvHorrorBackstoryDialogue : Dialogue {
        public StvHorrorBackstoryDialogue() {
            Say(Uwu.Uwufy("Hello there! I am Steve E Horror")); 
            Say(Uwu.Uwufy("Can you understand what I am saying?"))
            .Choice(new TextOption("What are you saying?")
                .IfChosen(new TriggerDialogueAction<StvHorrorSad>()))
            .Choice(new TextOption("Yes, I believe I can")
                .IfChosen(new TriggerDialogueAction<StvHorrorExp>()));        
        }
    }

    public class StvHorrorSad : Dialogue {
        public StvHorrorSad() {
            Say(Uwu.Uwufy("Oh no!"));
            Say(Uwu.Uwufy("It's like a curse, nobody can understand me :("));
            Say(Uwu.Uwufy("What can I do?"));
        }
    }

    public class StvHorrorExp : Dialogue {
        public StvHorrorExp() {
            SteveEHorrorDialogue s = SteveEHorrorDialogue.s;

            Say(Uwu.Uwufy("Oh, finally!"));
            Say(Uwu.Uwufy("I thought the day would never come!"));
            Say(Uwu.Uwufy("You know, because of the way I speak nobody gets what I am saying"));
            Say(Uwu.Uwufy("It's really frustrating; they dont even take me serious"))
            .DoAfter(GiveItem(s._setve_horror_backstory))
            .DoAfter(new TriggerDialogueAction<StvHorrorChoice>());
        }
    }

    public class StvHorrorChoice : Dialogue {
        public StvHorrorChoice() {

            Say(Uwu.Uwufy("Have you got an Idea what I could do?"))
            .Choice(new TextOption("Sorry, I have no Idea")
                .IfChosen(new TriggerDialogueAction<StvHorrorBye>()))
            .Choice(new ItemOption(s.void_bar)
                .IfChosen(new TriggerDialogueAction<StvHorrorVoid>()))
            .Choice(new ItemOption(s.cutecoin)
                .IfChosen(new TriggerDialogueAction<StvHorrorCoin>()))
            .Choice(new ItemOption(s.startcoin)
                .IfChosen(new TriggerDialogueAction<StvHorrorCoin>()))
            .Choice(new ItemOption(s.horrorcoin)
                .IfChosen(new TriggerDialogueAction<StvHorrorCoin>()))
            .Choice(new ItemOption(s.babelfisch)
                .IfChosen(new TriggerDialogueAction<StvHorrorBabel>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<StvHorrorItem>()));
        }
    }

    public class StvHorrorBabel : Dialogue {
        public StvHorrorBabel() {
            SteveEHorrorDialogue s = SteveEHorrorDialogue.s;

            Say(Uwu.Uwufy("Oh, what is this?"));
            Say(Uwu.Uwufy("*crunch* *chomp*"));
    
            Say("Mmh, delicious");
            Say("WAIT A MINUTE");
            Say("Am I actually able to speak normally again?");
            Say("This is amazing, thank you so much!");
            Say("As my token of gratitude please have this coin.")
            .DoAfter(RemoveItem(s.babelfisch))
            .DoAfter(GiveItem(s.horrorcoin))
            .DoAfter(GiveItem(s._given_horrorcoin));
        }
    }

    public class StvHorrorDefaultDialogue : Dialogue {
        public StvHorrorDefaultDialogue() {
            Say(Uwu.Uwufy("Oh, you've returned"))
            .DoAfter(new TriggerDialogueAction<StvHorrorChoice>());
        }
    }

    public class StvHorrorBye : Dialogue {
        public StvHorrorBye() {
            Say(Uwu.Uwufy("Alright but please come back"));
            Say(Uwu.Uwufy("I really want to be able to talk to people"));
            Say(Uwu.Uwufy("See you"));
        }
    }

    public class StvHorrorHappyDialogue : Dialogue {
        public StvHorrorHappyDialogue() {
            Say("I am so grateful that I can finally speak normal again!");
            Say("You are welcome in my Shop anytime!");
        }
    }

    public class StvHorrorCoin : Dialogue {
        public StvHorrorCoin() {
            Say(Uwu.Uwufy("So you are a customer?"));
            Say(Uwu.Uwufy("I have a lot of special items in my shop"));
            Say(Uwu.Uwufy("Are you perhaps interested in Terry the terrible torture turtle?"));
            Say(Uwu.Uwufy("There are a lot of other Items you can buy as well!"))
            .DoAfter(new TriggerDialogueAction<StvHorrorChoice>());
        }
    }

    public class StvHorrorVoid : Dialogue {
        public StvHorrorVoid() {
            Say(Uwu.Uwufy("Thanks but I am afraid I can't take that"));
            Say(Uwu.Uwufy("I am on a very strict diet you know, I can only eat fish"))
            .DoAfter(new TriggerDialogueAction<StvHorrorChoice>());
        }
    }

    public class StvHorrorItem : Dialogue {
        public StvHorrorItem() {
            Say(Uwu.Uwufy("I don't have a use for that, keep it"))
            .DoAfter(new TriggerDialogueAction<StvHorrorChoice>());
        }
    }
}
