using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinelessDudeDilaogue : DialogueTrigger
{
    public Item grease;
    public Item bloodbucket;
    public Item emptybucket;
    public Item _spineless_and_lifeless;
    public Item maiddress;
    public Item dirty_maiddress;
    public Item clean_apron;
    public Item dirty_apron;

    public static SpinelessDudeDilaogue s;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        SpinelessDudeDilaogue.s = this;

        if (! Inventory.Instance.HasItem(_spineless_and_lifeless)) {
            return new SpinelessDudeHi();
        }
        return new SpinelessDudeHappy();
    }

    public class SpinelessDudeHi : Dialogue {
        public SpinelessDudeHi() {
            Say("Hey there!");
            Say("As you can see they are getting properly tortured");
            Say("However the same can't be said for me");
            Say("This machine is ancient and the mechanism is rusty");
            Say("And the customer service is abysmal, I don't want to be tortured *that* way");
            Say("If only there was a way to make it work again")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeChoice : Dialogue {
        public SpinelessDudeChoice() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Have you got an idea how to get the mechanism working again?")
            .Choice(new TextOption("No, sorry ...")
                .IfChosen(new TriggerDialogueAction<SpinelessDudeBye>()))
            .Choice(new TextOption("Are you OK?")
                .IfChosen(new TriggerDialogueAction<SpinelessDudeOk>()))
            .Choice(new ItemOption(s.bloodbucket)
                .IfChosen(new TriggerDialogueAction<SpinelessDudeBloodBucket>()))
            .Choice(new ItemOption(s.grease)
                .IfChosen(new TriggerDialogueAction<SpinelessDudeGrease>()))
            .Choice(new ItemOption(s.maiddress)
                .IfChosen(new TriggerDialogueAction<SpinelessDudeMaiddress>()))
            .Choice(new ItemOption(s.clean_apron)
                .IfChosen(new TriggerDialogueAction<SpinelessDudeApron>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<SpinelessDudeItem>()));
        }
    }

    public class SpinelessDudeBye : Dialogue {
        public SpinelessDudeBye() {
            Say("See you");
            Say("But please find something, I want to be tortured like them");
            Say("It's a real torture to only see and not feel it");
        }
    }

    public class SpinelessDudeOk : Dialogue {
        public SpinelessDudeOk() {
            Say("Yeah?");
            Say("Is something wrong?");
            Say("Is it not natural wanting to get tortured?");
            Say("What a strange question")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeBloodBucket : Dialogue {
        public SpinelessDudeBloodBucket() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Ooooh thats a good amount of blood!");
            Say("Where'd you get that from or is it yours?")
            .Choice(new TextOption("..."));
            Say("Anyway... do you think we can use the blood as lubricant for the mechanism?")
            .DoAfter(RemoveItem(s.bloodbucket))
            .DoAfter(GiveItem(s.emptybucket));
            Say("*creaaaak* *screeeeeeeeetch*");
            Say("Aaaah my poor ears");
            Say("That didn't seem to work")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeItem : Dialogue {
        public SpinelessDudeItem() {
            Say("Hm this doesn't look useful at all")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeMaiddress : Dialogue {
        public SpinelessDudeMaiddress() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Hm that's an interesting idea; maybe cleaning it will help")
            .DoAfter(RemoveItem(s.maiddress))
            .DoAfter(GiveItem(s.dirty_maiddress));
            Say("*screeeeeeeeetch* *creaaaak* *screeeeeeeeetch*");
            Say("Nooooo my ears!");
            Say("Well that didn't work and only made the dress dirty")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeApron : Dialogue {
        public SpinelessDudeApron() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Hm that's an interesting idea; maybe cleaning it will help")
            .DoAfter(RemoveItem(s.clean_apron))
            .DoAfter(GiveItem(s.dirty_apron));
            Say(" *creaaaak* *screeeeeeeeetch*  *screeeeeeeeetch* *creaaaak*");
            Say("Ugh what a horrible noise!");
            Say("It still doesn't work and only made the apron dirty")
            .DoAfter(new TriggerDialogueAction<SpinelessDudeChoice>());
        }
    }

    public class SpinelessDudeGrease : Dialogue {
        public SpinelessDudeGrease() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Oh that could really work!")
            .DoAfter(RemoveItem(s.grease))
            .DoAfter(GiveItem(s._spineless_and_lifeless));
            Say("Aeugh *huff* Aaaaaaah *gasps* YESSSSS");
            Say("Thaaaanks ssso muchhh");
        }
    }

    public class SpinelessDudeHappy : Dialogue {
        public SpinelessDudeHappy() {
            Say("*Noises of intense pain and enjoyment*");
        }
    }
}
