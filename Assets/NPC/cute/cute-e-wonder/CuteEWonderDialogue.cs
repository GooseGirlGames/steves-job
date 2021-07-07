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
        Goose:  Nice goose, but won't help
        Bloody Goose:  Poor creature
        Geese Greese:  Poor creature
        Cocktail:  Goose head :(
*/

public class CuteEWonderDialogue : DialogueTrigger {
    public static CuteEWonderDialogue t;
    public Item spine;
    public Item magpie;
    public Item goose;
    public Item bloodgoose;
    public Item grease;
    public Item cocktail;  // TODO remove if goosehead in period cocktail won't become cannon
    public Item spineGiven;
    public Item problemExplained;
    public List<Item> shinyItems;
    new private Renderer renderer;

    private void Awake() {
        renderer = GetComponent<Renderer>();
    }

    public override Dialogue GetActiveDialogue() {
        CuteEWonderDialogue.t = this;

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

    private void TriggerChickenAnimation() {
        // TODO Let a chicken fly away from the store
    }

    public class HelpMe : Dialogue {
        public HelpMe() {
            Say(
                "H-Hey you, can I bother you for a sec? "
                + "There's a big m-monster in my store, a-and I can't go in there anymore."
            );

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
            .Do(RemoveItem(t.spine))
            .Do(GiveItem(t.spineGiven));

            Say("Wish me luck, I'll be right back...")
            .DoAfter(() => { t.renderer.enabled = false; });

            Say("...").DoAfter(t.TriggerChickenAnimation);

            Say("......")
            .DoAfter(() => { t.renderer.enabled = true; });

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


            EmptySentence()
            .Do(GiveItem(t.spineGiven))
            .Do(new TriggerDialogueAction<Thanks>());
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
                + "Now I get what that whole '" + t.name + ", you're so spineless' "
                + "business was about!"
            );
        }
    }

    public class NiceGoose : Dialogue {

        public NiceGoose() {
            Say(
                "Aww, what an adorable little specimen! \n"
                + "These little h-honkers always cheer me up."
            );

            Say("Let me see what I can do about that monster...")
            .DoAfter(() => { t.renderer.enabled = false; });

            Say("...AAAAAAAAAAAAAAAAAAAAAAAAAAAAAHHHHHHHHHHH!!!")
            .DoAfter(() => { t.renderer.enabled = true; });

            Say("Okay okay, that did not work out. God, why am I so easily scared?")
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
                + "Sorry to be bothering you with this..."
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
