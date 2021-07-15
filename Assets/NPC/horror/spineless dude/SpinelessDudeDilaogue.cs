using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinelessDudeDilaogue : DialogueTrigger
{
    public Item grease;
    public Item spine;
    public Item bloodbucket;
    public Item cute_bucket_empty;
    public Item cute_bucket_full;
    public Item emptybucket;
    public Item maiddress;
    public Item dirty_maiddress;
    public Item clean_apron;
    public Item dirty_apron;

    public Item _spineless_and_lifeless;
    public Item _got_spine;
    public Animator animator;
    public Sprite ava_bloody;
    public Sprite ava_bloody_nospine;
    public Sprite ava_norm;

    public static SpinelessDudeDilaogue s;

    void Awake() {
        UpdateSpineyness();
        Instance = this;
    }

    public void UpdateSpineyness() {
        avatar = ava_norm;
        if (Inventory.Instance.HasItem(_spineless_and_lifeless)) {
            animator.SetTrigger("Greased");
            avatar = ava_bloody;
        }
        if (Inventory.Instance.HasItem(_got_spine)) {
            animator.SetTrigger("Removed");
            avatar = ava_bloody_nospine;
            name = "Despined Fionn";
        }
    }

    public override Dialogue GetActiveDialogue() {
        SpinelessDudeDilaogue.s = this;
        UpdateSpineyness();

        if (!Inventory.Instance.HasItem(_spineless_and_lifeless)) {
            return new SpinelessDudeHi();
        }
        if (Inventory.Instance.HasItem(_got_spine)) {
            return new SpinelessDudeDead();
        }
        return new SpinelessDudeHappy();
    }

    public class SpinelessDudeHi : Dialogue {
        public SpinelessDudeHi() {
            Say("Hey there!");
            Say("I'm in dire need for a nice stretching routine");
            //Say("As you can see they are getting properly tortured");
            //Say("However the same can't be said for me");
            Say("However, this machine is ancient and the mechanism is rusty");
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
            .Choice(new ItemOption(s.cute_bucket_empty)
                .IfChosen(new TriggerDialogueAction<SpinelessDudeCuteBloodBucket>()))
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

    public class SpinelessDudeCuteBloodBucket : Dialogue {
        public SpinelessDudeCuteBloodBucket() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("Ooooh thats a good amount of blood!");
            Say("Where'd you get that from or is it yours?")
            .Choice(new TextOption("..."));
            Say("Anyway... do you think we can use the blood as lubricant for the mechanism?")
            .DoAfter(RemoveItem(s.cute_bucket_full))
            .DoAfter(GiveItem(s.cute_bucket_empty));
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
            Say("Aeugh *huff* Aaaaaaah *gasps* YESSSSS")
            .Do(s.UpdateSpineyness);
            Say("Thaaaanks ssso muchhh");
            Say("I have no use for that spine anymore..just take it if you need some courage")
            .Choice(
                new TextOption("Umm, maybe later")
            )
            .Choice(
                new TextOption("Take Spine")
                .IfChosen(new TriggerDialogueAction<SpinelessDudeHappy>())
            );
        }
    }

    public class SpinelessDudeHappy : Dialogue {
        public SpinelessDudeHappy() {
            SpinelessDudeDilaogue s = SpinelessDudeDilaogue.s;

            Say("*Noises of intense pain and enjoyment*")
            .DoAfter(GiveItem(s.spine))
            .DoAfter(GiveItem(s._got_spine));
        }
    }

    public class SpinelessDudeDead : Dialogue {
        public SpinelessDudeDead() {
            Say("...");
        }
    }
}
