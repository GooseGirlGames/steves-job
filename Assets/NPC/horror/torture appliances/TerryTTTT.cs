using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerryTTTT : DialogueTrigger
{
    public Item _given_horrorcoin;
    public Item horrorcoin;
    public Item startcoin;
    public Item cutecoin;

    public const string DefName = "Steve E Horror";
    public const string UwuName = "Steve E Howwow";

    public static TerryTTTT t;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        TerryTTTT.t = this;

        if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            name = DefName;
            return new TerryTTTTDescription();
        }
        name = UwuName;
        return new TerryTTTTHandsOff();
    }

    public class TerryTTTTHandsOff : Dialogue {
        public TerryTTTTHandsOff() {
            Say(Uwu.Uwufy("Hey!"));
            Say(Uwu.Uwufy("Hands off of my valuable product"));
        }
    }

    public class TerryTTTTDescription : Dialogue {
        public TerryTTTTDescription() {
            Say("This is 'Terry The Terrible Torture Tortoise'");
            Say("Not to be cofused with 'Terry The Terrible Torture Turtle'");
            Say("Although they might look similar one of them is much more dangerous");
            Say("But I forgot which one...")
            .DoAfter(new TriggerDialogueAction<TerryTTTTChoice>());
        }
    }

    public class TerryTTTTChoice : Dialogue {
        public TerryTTTTChoice() {
            TerryTTTT t = TerryTTTT.t;

            Say("This one can be bought for only 200 coins")
            .Choice(new TextOption("I don't think I can use that..."))
            .Choice(new ItemOption(t.horrorcoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTTMoney>()))
            .Choice(new ItemOption(t.startcoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTTMoney>()))
            .Choice(new ItemOption(t.cutecoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTTMoney>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<TerryTTTTUseless>()));
        }
    }

    public class TerryTTTTMoney : Dialogue {
        public TerryTTTTMoney() {
            Say("Hm you can't afford Terry with just that");
        }
    }

    public class TerryTTTTUseless : Dialogue {
        public TerryTTTTUseless() {
            Say("You can't pay with that, sorry");
        }
    }
}
