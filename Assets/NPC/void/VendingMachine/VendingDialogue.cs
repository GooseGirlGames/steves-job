using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingDialogue : DialogueTrigger{
    public const string VENDINGMACHINE_NAME = "AQUACLAW";
    public Item item_for_sale;
    public Item startcoin;
    public Item cutecoin;
    public Item horrorcoin;

    private void Awake() {
        this.name = VENDINGMACHINE_NAME;
    }

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.CoinBalance() == 0) {
            return new NoCoins();
        }

        if (!Inventory.Instance.HasItem(item_for_sale)) {
            return new BuyItem(this);
        } else {
            return new ItemBought();
        }
    }

    public class BuyItem : Dialogue {
        public BuyItem(VendingDialogue t) {
            EmptySentence().Do(new TriggerDialogueAction<Welcome>());

            Say("BuyItem?")
            .Choice(
                new ItemOption(t.startcoin)
                .IfChosen(RemoveItem(t.startcoin))
                .IfChosen(GiveItem(t.item_for_sale))
            )
            .Choice(
                new ItemOption(t.horrorcoin)
                .IfChosen(RemoveItem(t.horrorcoin))
                .IfChosen(GiveItem(t.item_for_sale))
            )
            .Choice(
                new ItemOption(t.cutecoin)
                .IfChosen(RemoveItem(t.cutecoin))
                .IfChosen(GiveItem(t.item_for_sale))
            )
            .Choice(
                new TextOption("No")
            )
            .DoAfter(new TriggerDialogueAction<Bye>());
        }
    }


    public class ItemBought : Dialogue {
        public ItemBought() {
            Say("ItemBought");
        }
    }


    public class NoCoins : Dialogue {
        public NoCoins() {
            Say("NoCoins");
        }
    }

    public class Welcome : Dialogue {
        public Welcome() {
            Say("Welcome to the " + VENDINGMACHINE_NAME + "!");
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            Say("Thank you for using " + VENDINGMACHINE_NAME + "!");
        }
    }


}