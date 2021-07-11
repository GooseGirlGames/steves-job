using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanDialogue : DialogueTrigger
{
    public static HangmanDialogue h;
    public Item key;
    public Item _key_given;
    public Item _hangman_story;

    void Awake() {
        Instance = this;
    }

    /*
    Hangman personality traits:
    #TODO 
    */

    public override Dialogue GetActiveDialogue() {
        HangmanDialogue.h = this;

        if (! Inventory.Instance.HasItem(_hangman_story)) {
            if (Inventory.Instance.HasItem(_key_given)) {
                return new HangmanFinishedDialogue();
            }
            return new Introduction();
        }
        return new DefaultDialogue();
    }

    public class DefaultDialogue : Dialogue {
        public DefaultDialogue() {
            HangmanDialogue h = HangmanDialogue.h;

            EmptySentence().Do(new TriggerDialogueAction<Introduction>());

            Say("Have you seen my key by any chance?")
            .Choice(new ItemOption(h.key)
                .IfChosen(new TriggerDialogueAction<Key>()))
            .Choice(new TextOption("Sorry, I haven't")
                .IfChosen(new TriggerDialogueAction<Bye>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<NoKey>()));
        }
    }

    public class Introduction : Dialogue {
        public Introduction() {
            HangmanDialogue h = HangmanDialogue.h;

            Say("When I was ");
            Say("A young boy");
            Say("My father");
            Say("told me to be careful with keys");
            Say("And never loose them");
            Say("But guess what happened ...")
            .DoAfter(GiveItem(h._hangman_story))
            .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }

    public class Key : Dialogue {
        public Key() {
            HangmanDialogue h = HangmanDialogue.h;

            Say("Oh wow, that is actually my key")
            .DoAfter(RemoveItem(h.key))
            .DoAfter(GiveItem(h._key_given));
            Say("Where did you find it?")
            .Choice(new TextOption("So there was this racoon..."))
            .Choice(new TextOption("It fell from the sky"))
            .Choice(new TextOption("You wouldn't believe it"));
            Say("Huh...");
            Say("but thank you so much");
            Say("now i can finally enter my shop again");
            Say("and i am not a disgrace to my father anymore");
            Say("well at least not in that aspect");
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            Say("See you soon");
            Say("and keep an eye out for my key");
        }
    }

    public class NoKey : Dialogue {
        public NoKey() {
            Say("That is not a key, can't you tell?")
            .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }

    public class HangmanFinishedDialogue : Dialogue {
        public HangmanFinishedDialogue() {
            Say("*humm* *humm*");
            Say("I got my key back");
            Say("*humm* *humm*");
            Say("I'll take good care, I promise");
            Say("*humm* *humm*");
        }
    }
}
