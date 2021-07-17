using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTGDialogue : DialogueTrigger
{
    public Item goose;
    public Item goose_blood;
    public Item goose_blood_bow;
    public Item goose_bow;
    public Item grease;
    public Item startcoin;
    public Item cutecoin;
    public Item horrorcoin;
    public Item _gtg_active;
    public static GTGDialogue g;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        GTGDialogue.g = this;

        if (Inventory.Instance.HasItem(_gtg_active)) {
            return new GTGactiveDialogue();
        }
        return new GTGpassiveDialogue();
    }


    public class GTGactiveDialogue : Dialogue {
        public GTGactiveDialogue() {
            GTGDialogue g = GTGDialogue.g;

            Say("This is an activated \"Geese to Grease\" machine.")
            .Choice(new ItemOption(g.horrorcoin)
                .IfChosen(new TriggerDialogueAction<Coin_Again>()))
            .Choice(new ItemOption(g.startcoin)
                .IfChosen(new TriggerDialogueAction<Coin_Again>()))
            .Choice(new ItemOption(g.cutecoin)
                .IfChosen(new TriggerDialogueAction<Coin_Again>()))
            .Choice(new ItemOption(g.goose)
                .IfChosen(new TriggerDialogueAction<Gooose_Action>()))
            .Choice(new ItemOption(g.goose_blood)
                .IfChosen(new TriggerDialogueAction<Gooose_Action>()))
            .Choice(new ItemOption(g.goose_blood_bow)
                .IfChosen(new TriggerDialogueAction<Gooose_Action>()))
            .Choice(new ItemOption(g.goose_bow)
                .IfChosen(new TriggerDialogueAction<Gooose_Action>()))
            .Choice(new TextOption("Leave"))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<Invalid_Item>()));
        }
    }

    public class GTGpassiveDialogue : Dialogue {
        public GTGpassiveDialogue() {
            GTGDialogue g = GTGDialogue.g;

            Say("This is a \"Geese to Grease\" machine which is not activated.")
                .Choice(new ItemOption(g.horrorcoin)
                    .IfChosen(RemoveItem(g.horrorcoin))
                    .IfChosen(new TriggerDialogueAction<Coin>()))
                .Choice(new ItemOption(g.startcoin)
                    .IfChosen(RemoveItem(g.startcoin))
                    .IfChosen(new TriggerDialogueAction<Coin>()))
                .Choice(new ItemOption(g.cutecoin)
                    .IfChosen(RemoveItem(g.cutecoin))
                    .IfChosen(new TriggerDialogueAction<Coin>()))
                .Choice(new ItemOption(g.goose)
                    .IfChosen(new TriggerDialogueAction<Goose_Coin>()))
                .Choice(new ItemOption(g.goose_blood)
                    .IfChosen(new TriggerDialogueAction<Goose_Coin>()))
                .Choice(new ItemOption(g.goose_blood_bow)
                    .IfChosen(new TriggerDialogueAction<Goose_Coin>()))
                .Choice(new ItemOption(g.goose_bow)
                    .IfChosen(new TriggerDialogueAction<Goose_Coin>()))
                .Choice(new TextOption("Inspect")
                    .IfChosen(new TriggerDialogueAction<Inspect_GTG>()))
                .Choice(new TextOption("Leave"))
                .Choice(new OtherItemOption()
                    .IfChosen(new TriggerDialogueAction<No_Coin>()));
        }
       
    }

    public class Coin : Dialogue {
        public Coin() {
            GTGDialogue g = GTGDialogue.g;

            Say("*rumble* *rumble* you have successfully activated the machine.")
            .DoAfter(GiveItem(g._gtg_active))
            .DoAfter(new TriggerDialogueAction<GTGactiveDialogue>());
        }
    }

    public class Goose_Coin : Dialogue {
        public Goose_Coin() {
            Say("This machine transforms geese, however it can't be activated by one.")
            .DoAfter(new TriggerDialogueAction<GTGpassiveDialogue>());
        }
    }

    public class Inspect_GTG : Dialogue {
        public Inspect_GTG() {
            Say("There seems to be a coin slot on the side of the machine.")
            .DoAfter(new TriggerDialogueAction<GTGpassiveDialogue>());
        }
    }

    public class No_Coin : Dialogue {
        public No_Coin() {
            Say(
                DialogueManager.Instance.currentItem.name +
                " does not seem to be able to activate the machine."
            )
            .DoAfter(new TriggerDialogueAction<GTGpassiveDialogue>());
        }
    }

    public class Coin_Again : Dialogue {
        public Coin_Again() {
            Say("the machine is already activated, no need for that.")
            .DoAfter(new TriggerDialogueAction<GTGactiveDialogue>());
        }
    }

    public class Invalid_Item : Dialogue {
        public Invalid_Item() {
            Say("this does not seem like a usable item, does it?")
            .DoAfter(new TriggerDialogueAction<GTGactiveDialogue>());
        }
    }

    public class Gooose_Action : Dialogue {
        public Gooose_Action() {
            GTGDialogue g = GTGDialogue.g;
            
            Say("the machine screetches a bit and the goose makes some unusual sounds...")
            .DoAfter(RemoveItem(DialogueManager.Instance.currentItem))
            .DoAfter(GiveItem(g.grease));
        }
    }
}
