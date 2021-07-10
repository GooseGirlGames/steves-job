using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMaiden : DialogueTrigger
{
    public Item _given_horrorcoin;
    public Item horrorcoin;
    public Item startcoin;
    public Item cutecoin;

    public const string DefName = "Steve E Horror";
    public const string UwuName = "Steve E Howwow";

    public static IronMaiden i;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        IronMaiden.i = this;

        if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            name = DefName;
            return new IronMaidenDesc();
        }
        name = UwuName;
        return new IronMaidenHandsOff();
    }

    public class IronMaidenDesc : Dialogue {
        public IronMaidenDesc() {
            Say("Another medieval torture instrument");
            Say("This is a iron maiden");
            Say("like this band you might know");
            Say("but compared to them the screams that come out of that thing");
            Say("are actual music to my ears");
            Say("It's quite effective, you should try it");
            Say("Or try it on someone")
            .DoAfter(new TriggerDialogueAction<IronMaidenChoice>());
        }
    }

    public class IronMaidenChoice : Dialogue {
        public IronMaidenChoice() {
            IronMaiden i = IronMaiden.i;

            Say("It's completely made up of steel, hence very valuable");
            Say("I sell it for 10.000c, are you interested?")
            .Choice(new TextOption("..."))
            .Choice(new ItemOption(i.horrorcoin)
                .IfChosen(new TriggerDialogueAction<IronMaidenCoin>()))
            .Choice(new ItemOption(i.cutecoin)
                .IfChosen(new TriggerDialogueAction<IronMaidenCoin>()))
            .Choice(new ItemOption(i.startcoin)
                .IfChosen(new TriggerDialogueAction<IronMaidenCoin>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<IronMaidemItem>()));
        }
    }

    public class IronMaidenCoin : Dialogue {
        public IronMaidenCoin() {
            Say("Do you want to buy a spike of the iron maiden");
            Say("or the thing itself?");
            Say("Beacuse that ain't nearly enough");
        }
    }

    public class IronMaidemItem : Dialogue {
        public IronMaidemItem() {
            Say("Do you want to pay with that?");
            Say("I am afraid that is not possible");
        }
    }

    public class IronMaidenHandsOff : Dialogue {
        public IronMaidenHandsOff() {
            Say(Uwu.Uwufy("Don't you dare touch it"));
            Say(Uwu.Uwufy("I am not paying if you hurt yourself with that"));
            Say(Uwu.Uwufy("but your cleaning it if you make it dirty"));
        }
    }
}
