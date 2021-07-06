using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawmachineDialogue : DialogueTrigger {
    public const string CLAWMACHINE_NAME = "AQUACLAW";
    public static ClawmachineDialogue t;
    public Item item_for_sale;
    public Item startcoin;
    public Item cutecoin;
    public Item horrorcoin;
    public Item machine_used;

    private void Awake() {
        this.name = CLAWMACHINE_NAME;
    }

    public override Dialogue GetActiveDialogue() {
        ClawmachineDialogue.t = this;

        if (Inventory.Instance.HasItem(machine_used)) {
            return new Bye();
        }

        if (Inventory.Instance.CoinBalance() == 0) {
            return new NoCoins();
        }

        return new BuyItem();
    }

    public class BuyItem : Dialogue {
        public BuyItem() {
            ClawmachineDialogue t = ClawmachineDialogue.t;

            EmptySentence().Do(new TriggerDialogueAction<Welcome>());

            Say("BuyItem?")
            .Choice(
                new ItemOption(t.startcoin)
                .IfChosen(RemoveItem(t.startcoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
            )
            .Choice(
                new ItemOption(t.horrorcoin)
                .IfChosen(RemoveItem(t.horrorcoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
            )
            .Choice(
                new ItemOption(t.cutecoin)
                .IfChosen(RemoveItem(t.cutecoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
            )
            .Choice(
                new TextOption("No")
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<NotACoin>())
            )
            .DoAfter(new TriggerDialogueAction<Bye>());
        }
    }

    public class NoCoins : Dialogue {
        public NoCoins() {
            Say("My goods are only available in exchange for legal tender!");
        }
    }

    public class Welcome : Dialogue {
        public Welcome() {
            Say("Welcome to the " + CLAWMACHINE_NAME + "!");
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            ClawmachineDialogue t = ClawmachineDialogue.t;

            Say("Thank you for using " + CLAWMACHINE_NAME + "!");

            Say("Have fun with your brand new " + t.item_for_sale.name + "!")
            .If(HasItem(t.item_for_sale));
        }
    }

    public class NotACoin : Dialogue {
        public NotACoin() {
            ClawmachineDialogue t = ClawmachineDialogue.t;

            Say(
                "Sadly, I cannot accept "
                + DialogueManager.Instance.currentItem.name
                + " as currency."
            )
            .DoAfter(new TriggerDialogueAction<BuyItem>());
        }
    }

}
