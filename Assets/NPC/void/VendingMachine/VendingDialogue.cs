using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingDialogue : DialogueTrigger{
    
    public const string BORKED = "☎";
    public const string VENDINGMACHINE_NAME = BORKED + BORKED + BORKED + BORKED + "-O-MAT";
    public static VendingDialogue t;
    public Item item_for_sale;
    public Item startcoin;
    public Item cutecoin;
    public Item horrorcoin;
    public Item _used;

    private void Awake() {
        this.name = VENDINGMACHINE_NAME;
    }

    public override Dialogue GetActiveDialogue() {
        t = this;
        if (!Inventory.Instance.HasItem(_used)) {
            return new Welcome();
        } else {
            return new Bye();
        }
    }

    public class BuyItem : Dialogue {
        public BuyItem() {
            Say(
                "Are you feeling hungry? Has reality ruptured? You're in luck, "
                + "because that just means it's the perfect time for a nicely sweet "
                + t.item_for_sale.name.ToUpper() + "!"
            )
            .Choice(
                new ItemOption(t.startcoin)
                .IfChosen(RemoveItem(t.startcoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t._used))
            )
            .Choice(
                new ItemOption(t.horrorcoin)
                .IfChosen(RemoveItem(t.horrorcoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t._used))
            )
            .Choice(
                new ItemOption(t.cutecoin)
                .IfChosen(RemoveItem(t.cutecoin))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t._used))
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<NotACoin>())
            )
            .Choice(
                new TextOption("No")
            )
            .DoAfter(new TriggerDialogueAction<Bye>());
        }
    }



    public class NotACoin : Dialogue {
        public NotACoin() {
            Say(
                "You may only pay me in United States Dollars or "
                + BORKED + BORKED + BORKED + BORKED + BORKED + BORKED
                + " coins."
            )
            .DoAfter(
                new TriggerDialogueAction<BuyItem>()
            );
        }
    }

    public class Welcome : Dialogue {
        public Welcome() {
            Say(
                "Welcome to the " + VENDINGMACHINE_NAME + ", proudly sponsored by the "
                + BORKED + BORKED + BORKED + BORKED + BORKED + " "
                + "corporation!"
            )
            .DoAfter(
                new TriggerDialogueAction<BuyItem>()
            );
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            Say("Thank you for using " + VENDINGMACHINE_NAME + "!");
        }
    }


}