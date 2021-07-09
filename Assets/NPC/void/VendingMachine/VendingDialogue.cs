using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingDialogue : DialogueTrigger{
    
    public const string BORKED = "□";
    public const string VENDINGMACHINE_NAME = BORKED + BORKED + BORKED + BORKED + "-O-MAT";
    public const string VOIDCORP =
            BORKED + BORKED + BORKED + BORKED + BORKED + BORKED + " CORPORATION";
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

    public class ChooseItem : Dialogue {
        public ChooseItem() {
            Say(
                "Are you feeling hungry? Has reality ruptured? You're in luck, "
                + "because that just means it's the perfect time for a nice "
                + t.item_for_sale.name.ToUpper() + "!  Or perhaps a tasty "
                + t.item_for_sale.name.ToUpper() + " or "
                + t.item_for_sale.name.ToUpper() + "?"
            )
            .Choice(
                new TextOption(t.item_for_sale.name.ToUpper())
                .IfChosen(new TriggerDialogueAction<BuyItem>())
            )
            .Choice(
                new TextOption(t.item_for_sale.name.ToUpper())
                .IfChosen(new TriggerDialogueAction<BuyItem>())
            )
            .Choice(
                new TextOption(t.item_for_sale.name.ToUpper())
                .IfChosen(new TriggerDialogueAction<BuyItem>())
            )
            .Choice(new TextOption("Nope"));
        }
    }

    public class BuyItem : Dialogue {
        public BuyItem() {
            Say(
                "Fourtanetley for " + VOIDCORP + " "
                + "capitalism has turned out to be a constant in our universe. "
                + "Please feed me currency."
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
                "You may only purchase "
                + t.item_for_sale.name.ToUpper() + " "
                + "with United States Dollars or "
                + BORKED + BORKED + BORKED + BORKED + BORKED + BORKED + BORKED
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
                "Welcome to the " + VENDINGMACHINE_NAME + ", proudly sponsored by the " + VOIDCORP
            )
            .DoAfter(
                new TriggerDialogueAction<ChooseItem>()
            );
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            Say("Thank you for using " + VENDINGMACHINE_NAME + "!");
            Say("Hope you'll enjoy your " + t.item_for_sale.name.ToUpper())
            .If(HasItem(t._used))
            .If(HasItem(t.item_for_sale));
            Say(
                "Hope you've enjoyed your " + t.item_for_sale.name.ToUpper() + " "
                + "Would you like to leave some feedback?"
            )
            .Choice(
                new TextOption("Yes")
                .IfChosen(new TriggerDialogueAction<Feedback>())
            )
            .Choice(
                new TextOption("No")
            )
            .If(HasItem(t._used))
            .If(DoesNotHaveItem(t.item_for_sale));
        }
    }

    public class Feedback : Dialogue {
        public Feedback() {
            Say(
                "On a scale of 1-4, how intense were the beautiful memories of your childhood "
                + "induced by the consumption of " + t.item_for_sale.name.ToUpper() + "?"
            )
            .Choice(new TextOption("1 -- extremely intense"))
            .Choice(new TextOption("2 -- very intense"))
            .Choice(new TextOption("3 -- moderately intense"))
            .Choice(new TextOption("4 -- slightly intense"));

            Say(
                "On a scale of 1-4, how strongly has "+ t.item_for_sale.name.ToUpper() + " "
                + "exorcized vivid memories of socially uncomfortable situations?"
            )
            .Choice(new TextOption("1 -- extremely strongly"))
            .Choice(new TextOption("2 -- very strongly"))
            .Choice(new TextOption("3 -- moderately strongly"))
            .Choice(new TextOption("4 -- slightly strongly"));

            Say(
                "On a scale of 1-4, how subliminal has the influence of "
                + t.item_for_sale.name.ToUpper() + " advertising been to your subconsciousness?"
            )
            .Choice(new TextOption("1 -- extremely subliminal"))
            .Choice(new TextOption("2 -- very subliminal"))
            .Choice(new TextOption("3 -- moderately subliminal"))
            .Choice(new TextOption("4 -- slightly subliminal"));

            Say("Your feedback has been forwarded to " + VOIDCORP
            + " for consideration.");
            Say("Thank you for helping to improve " + t.item_for_sale.name.ToUpper());

        }
    }
}
