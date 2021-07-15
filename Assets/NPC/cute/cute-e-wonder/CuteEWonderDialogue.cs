using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Name:  Cute-E-Wonder
Store:  Birds Of Beauty
Problem:  Lacks confidence to remove rogue chicken from store
Items:
    Wants:  Spine
    Gives:  Magpie
    Invisible: _spinegiven
    Reacts to:
        Goose/Cute Goose:  Nice goose, but won't help
        Bloody Goose/Cute Bloody Goose:  Poor creature
        Geese Greese:  Poor creature
        Cocktail:  Goose head :(
*/

public class CuteEWonderDialogue : DialogueTrigger {
    public static CuteEWonderDialogue t;
    public Item spine;
    public Item magpie;
    public Item goose;
    public Item bloodgoose;
    public Item goosecute;
    public Item bloodgoosecute;
    public Item grease;
    public Item cocktail;
    public Item spineGiven;
    public Item cuteCoin;
    public Item problemExplained;
    public List<Item> shinyItems;
    new private Renderer renderer;
    public Sprite ava_initial;
    public Sprite ava_restored;
    private Animator animator;
    private const string LOCK_TAG = "Cute-E-Wonder";
    public Animator transitionAnimation;
    private bool restored = false; // temporary, for time in between spine handover and giving
                                   // _restored item (the latter of which triggers the restore
                                   // notification, which we want to happen during "Thanks")
    public Transform hintPosRestored;
    private void Awake() {
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        UpdateState();
    }

    private void UpdateState() {
        if (Inventory.Instance.HasItem(spineGiven) || restored) {
            avatar = ava_restored;
            animator.SetBool("Restored", true);
            hintPosition = hintPosRestored;
        } else {
            avatar = ava_initial;
            animator.SetBool("Restored", false);
        }
    }

    public override Dialogue GetActiveDialogue() {
        CuteEWonderDialogue.t = this;
        UpdateState();

        if (!Inventory.Instance.HasItem(spineGiven)) {
            if (!Inventory.Instance.HasItem(problemExplained)) {
                return new HelpMe();
            } else {
                return new GiveMeSpine();
            }
            
        } else {
            return new Thanks();
        }
    }

    private void CuteeEnter() {
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        player.Lock(LOCK_TAG);
        transitionAnimation.SetFloat("Speed", 1.6f);
        transitionAnimation.SetTrigger("ExitScene");
    }
    private void CuteeExit() {
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        player.Unlock(LOCK_TAG);
        transitionAnimation.SetTrigger("EnterScene");
        transitionAnimation.SetFloat("Speed", 1);
    }


    public class HelpMe : Dialogue {
        public HelpMe() {
            Say("H-Hey you, can I bother you for a sec?")
            .If(DoesNotHaveItem(t.problemExplained));
            
            Say("There's a big m-monster in my store, a-and I can't go in there anymore.");

            Say("It's just I don't-  I don't want to die...");

            Say("If only I had the courage to t-take care of this...")
            .Do(GiveItem(t.problemExplained))
            .DoAfter(new TriggerDialogueAction<GiveMeSpine>());
        }
    }

    public class GiveMeSpine : Dialogue {
        public GiveMeSpine() {
            Say(
                "Could you perhaps help me out... i-if that's okay?"
                + " Just n-need a little confidence boost..."
            )
            .Do(SpineHandover.CheckForShinyItem)  // We're doing this, so that `shinyItemName`
                                                  // is set *before* the instance of `SpineHandover`
                                                  // (and thus its sentences) is created.
            .Choice(new TextOption("..."))
            .Choice(
                new ItemOption(t.spine)
                .IfChosen(new TriggerDialogueAction<SpineHandover>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.goose)
                .IfChosen(new TriggerDialogueAction<NiceGoose>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.bloodgoose)
                .IfChosen(new TriggerDialogueAction<PoorCreature>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.goosecute)
                .IfChosen(new TriggerDialogueAction<NiceGoose>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.bloodgoosecute)
                .IfChosen(new TriggerDialogueAction<PoorCreature>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.grease)
                .IfChosen(new TriggerDialogueAction<PoorCreature>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(t.cocktail)
                .IfChosen(new TriggerDialogueAction<Cocktail>(exitCurrent: true))
            )
            .Choice(
                new TextOption("?")
                .IfChosen(new TriggerDialogueAction<HelpMe>(exitCurrent: true))
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<NoUse>(exitCurrent: true))
            );

            Say("Oh, okay... I-It's fine if you don't want to, you know. I'll be fine, I guess.");
        }
    }

    public class SpineHandover : Dialogue {
        private static string shinyItemName = "";
        private static bool hasShinyItem = false;
        public SpineHandover() {

            Say("Thanks, this'll help.")
            .Do(RemoveItem(t.spine));

            Say("Wish me luck, I'll be right back...")
            .DoAfter(t.CuteeEnter);

            Say("...");

            Say("......")
            .DoAfter(() => { 
                t.restored = true;
                t.UpdateState();
            })
            .DoAfter(t.CuteeExit);

            Say("Glad I got rid of that silly chicken!");
            Say("Almost trashed the whole place.");

            Say("Please accept this beautiful Eurasian magpie as a reward.")
            .Do(GiveItem(t.magpie));

            Say(
                "A truly magnificent creature, but don't let it go anywhere near that shiny "
                + SpineHandover.shinyItemName
                + " of yours."
            )
            .If(() => SpineHandover.hasShinyItem);
            Say(
                "A truly magnificent creature, but don't let it go anywhere near anything shiny"
            )
            .If(() => !SpineHandover.hasShinyItem);

            Say("Oh, and as a less feathery token of gratitude, here's some spare change.")
            .Do(GiveItem(t.cuteCoin));
            Say("I can't give you more than that sadly.");
            Say("That chicken must've eaten all that was left in my cash register.")
            .DoAfter(new TriggerDialogueAction<Thanks>());
        }

        public static void CheckForShinyItem() {
            Item shinyItem = Inventory.Instance.items.Find(
                (Item item) => t.shinyItems.Contains(item)
            );
            SpineHandover.hasShinyItem = shinyItem != null;
            if (SpineHandover.hasShinyItem) {
                SpineHandover.shinyItemName = shinyItem.name;
            } else {
                SpineHandover.shinyItemName = "";
            }
        }
    }

    public class Thanks : Dialogue {
        public Thanks() {
            Say(
                "Thanks again! \n"
                + "My back kinda hurts, but I'm super glad we got that whole spinelessness-"
                + "thing resolved!"
            ).Do(GiveItem(t.spineGiven));
        }
    }

    public class NiceGoose : Dialogue {

        public NiceGoose() {
            Say("Aww, what an adorable little specimen!");
            Say("These little h-honkers always cheer me up.");

            Say("Look at that cute bow!")
            .If(HasItem(t.goosecute));

            Say("Let me see what I can do about that monster...");

            Say("...")
            .Do(t.CuteeEnter);

            Say("...AAAAAAAAAAAAAAAAAAAAAAAAAAAAAHHHHHHHHHHH!!!")
            .DoAfter(t.CuteeExit);

            Say("Okay okay, that did not work out. God, why am I so easily scared?");

            Say("No wonder people call me weak and spineless...")
            .DoAfter(new TriggerDialogueAction<GiveMeSpine>());
        }
    }

    public class PoorCreature : Dialogue {
        public PoorCreature() {
            Say("Aaaaaahhhh wh-what have you done to that poor little creature?")
            .DoAfter(new TriggerDialogueAction<GiveMeSpine>());
        }
    }

    public class NoUse : Dialogue {
        public NoUse() {
            Say(
                "That's not g-going to help me... \n"
                + "Sorry to be bothering you with all this..."
            )
            .DoAfter(new TriggerDialogueAction<GiveMeSpine>());
        }
    }

    public class Cocktail : Dialogue {
        public Cocktail() {
            Say(
                "I-Is that a goose head sticking out of there? \n"
                + "I'm s-starting to feel real sick."
            )
            .DoAfter(new TriggerDialogueAction<GiveMeSpine>());
        }
    }

}
