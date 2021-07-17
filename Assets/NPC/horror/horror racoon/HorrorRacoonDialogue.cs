using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorRacoonDialogue : DialogueTrigger
{
    public Item bloodymary;
    public Item _horror_racoon_drink;
    public Item _horror_racoon_done;
    public Item _magpie_released;
    public Item _magpie_tauting;
    public Item bloodbucket;
    public Item emptybucket;
    public Item bloodbucketcute;
    public Item emptybucketcute;
    public Item grease;
    public Item goose;
    public Item goose_blood;
    public Item goose_blood_bow;
    public Item goose_bow;
    public Item magpie;
    public Item storekey;
    public Animator fadeToBlack;
    private Animator animator;
    public static HorrorRacoonDialogue h;
    private bool animationInProgress = false;
    private bool keysCanBeGrabbed = false;
    public const string LOCK_TAG = "Hungry horrible raccoon";

    void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();

        animator.SetBool("Big", Inventory.Instance.HasItem(_horror_racoon_drink));
        if (Inventory.Instance.HasItem(_horror_racoon_done)) {
            animator.SetTrigger("Schnumpzel");
        }
    }

    public override Dialogue GetActiveDialogue() {
        HorrorRacoonDialogue.h = this;

        if (animationInProgress)
            return null;

        if (Inventory.Instance.HasItem(_horror_racoon_done)) {
            return new CrunchyDialogue();
        }
        else if (Inventory.Instance.HasItem(_horror_racoon_drink)) {
            return new HorrorRacoonDefaultDialogue();
        }
        return new HorrorRacoonHi();
    }

    void SchnumpzelAnimation() {
        StartCoroutine(Schnumpzel());
    }
    private IEnumerator Schnumpzel() {
        stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();

        steve.Lock(LOCK_TAG);
        animationInProgress = true;
        keysCanBeGrabbed = true;
        fadeToBlack.SetFloat("Speed", 2.4f);
        animator.SetTrigger("Grab");
        yield return new WaitForSeconds(0.55f);

        foreach (Magpie m in GameObject.FindObjectsOfType<Magpie>()) {
            m.GetEaten();
        }
        fadeToBlack.SetTrigger("ExitScene");
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Schnumpzel");
        Inventory.Instance.AddItem(_horror_racoon_done);
        Inventory.Instance.RemoveItem(_magpie_released);
        Inventory.Instance.RemoveItem(_magpie_tauting);
        
        yield return new WaitForSeconds(0.5f);

        fadeToBlack.SetTrigger("EnterScene");
        fadeToBlack.SetFloat("Speed", 1);

        //yield return new WaitForSeconds(2.0f);
        animationInProgress = false;
        steve.Unlock(LOCK_TAG);
        DialogueManager.Instance.SetInstantTrue();
    }

    void TooSmall() {
        animator.SetTrigger("Grab");
    }

    public class CrunchyDialogue : Dialogue {
        public CrunchyDialogue() {
            Say("*crunch* *crunch* *crunch*");  // me_irl

            Say("*crunch* *crunch* Ouch, that's no good...")
            .If(() => h.keysCanBeGrabbed);

            Say("I didn't wanna eat *keys* for breakfast, ugh.")
            .If(() => h.keysCanBeGrabbed)
            .DoAfter(GiveItem(h.storekey));

            Say("*crunch* *crunch* *crunch*")
            .If(() => h.keysCanBeGrabbed)
            .DoAfter(() => { h.keysCanBeGrabbed = false; });
            //Say("Hm? I am taking an after-food nap, don't disturb me.");
            //Say("zZZzzZZZzzzzzzZzZ");
        }
    }

    public class HorrorRacoonHi : Dialogue {
        public HorrorRacoonHi() {
            Say("Hi there!");
            Say("I am a bit leached out beacuse I forgot to stay hydrated... And I haven't had breakfast yet...")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonDefaultDialogue : Dialogue {
        public HorrorRacoonDefaultDialogue() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

            Say("Have you found something I can eat or drink?")
            .Choice(new TextOption("I am afraid I haven't")
                .IfChosen(new TriggerDialogueAction<HorrorRacoonBye>()))
            .Choice(new ItemOption(h.bloodbucket)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonBucket>()))
            .Choice(new ItemOption(h.bloodbucketcute)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonBucket>()))
            .Choice(new ItemOption(h.goose)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.goose_blood)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.magpie)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.goose_blood_bow)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.goose_bow)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGoose>()))
            .Choice(new ItemOption(h.grease)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonGrease>()))
            .Choice(new ItemOption(h.bloodymary)
                .IfChosen(new TriggerDialogueAction<HorrorRacoonDrink>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<HorrorRacoonItem>()))
            .Choice(
                new TextOption("Magpie")
                .AddCondition(HasItem(h._magpie_released))
                .AddCondition(DoesNotHaveItem(h._magpie_tauting))
                .IfChosen(new TriggerDialogueAction<MagpieFarAway>())
            )
            .Choice(
                new TextOption("Magpie")
                .AddCondition(HasItem(h._magpie_tauting))
                .AddCondition(DoesNotHaveItem(h._horror_racoon_drink))
                .IfChosen(new TriggerDialogueAction<TooSmallForMagpie>())
            )
            .Choice(
                new TextOption("Magpie")
                .AddCondition(HasItem(h._magpie_tauting))
                .AddCondition(HasItem(h._horror_racoon_drink))
                .IfChosen(new TriggerDialogueAction<MagpieNom>())
            )
            ;
        }
    }

    public class HorrorRacoonBye : Dialogue {
        public HorrorRacoonBye() {
            Say("If you come across something edibale be sure to give it to me :)");
        }
    }

    public class HorrorRacoonBucket : Dialogue {
        public HorrorRacoonBucket() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;
            World bucketOrigin = DialogueManager.Instance.currentItem.originWorld;

            Say("Oh that doesn't look half bad!")
            .DoAfter(GiveItem(h.emptybucketcute))
            .If(() => bucketOrigin == World.Cute);
            Say("Oh that doesn't look half bad!")
            .DoAfter(GiveItem(h.emptybucket))
            .If(() => bucketOrigin != World.Cute);

            Say("*glug* *glug* *glug*");
            Say("Aaaaaah.");
            Say("It isn't half bad but I'd like something more refined.")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonGoose : Dialogue {
        public HorrorRacoonGoose() {
            Say("That's very nice of you, however recently I've started eating only free flying birds.");
            Say("I like the challenge and now eating birds just given to me feels wrong.")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonGrease : Dialogue {
        public HorrorRacoonGrease() {
            Say("I don't know, I am not a machine.");
            Say("Think I'll pass...")
            .DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonDrink : Dialogue {
        public HorrorRacoonDrink() {
            HorrorRacoonDialogue h = HorrorRacoonDialogue.h;

            Say("What's that?");
            Say("It smells amazing!");
            Say("*slurp* *slurp*")
            .Do(() => {h.animator.SetBool("Big", true);})
            .Do(GiveItem(h._horror_racoon_drink));
            Say("delicious!");
            Say("I feel energized again!");
                        Say("Thanks!")
            .DoAfter(RemoveItem(h.bloodymary));
            Say("Lemme know if something to eat shows up!");
            //.DoAfter(new TriggerDialogueAction<HorrorRacoonDefaultDialogue>());
        }
    }

    public class HorrorRacoonItem : Dialogue {
        public HorrorRacoonItem() {
            Say("Hm, looks interesting but... Nah.");
        }
    }

    
    public class TooSmallForMagpie : Dialogue {
        public TooSmallForMagpie() {
            Say("Mhhhh... *hrrrg*")
            .Do(h.TooSmall);
            Say("Maybe I should drink something first...");
        }
    }

    public class MagpieNom : Dialogue {
        public MagpieNom() {
            Say("What a tasty looking little magpie...")
            .DoAfter(h.SchnumpzelAnimation);

            //Say("*distressed magpie noise* ...... *bones crunching*")
            //.Do(RemoveItem(h._magpie_tauting));
            //Say("I don't really like the taste of keys, though. You can have these.")
            //.DoAfter(GiveItem(h.storekey));
        }
    }

    public class MagpieFarAway : Dialogue {
        public MagpieFarAway() {
            Say("Huh, a magpie? Sounds tasty, but can't you shoo him a littler closer to me?");
        }
    }
}
