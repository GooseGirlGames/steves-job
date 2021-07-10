using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerryTTT : DialogueTrigger
{
    public Item _given_horrorcoin;
    public Item horrorcoin;
    public Item startcoin;
    public Item cutecoin;

    public static TerryTTT t;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        TerryTTT.t = this;

        if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            return new TerryTTTDescription();
        }
        return new TerryTTTHandsOff();
    }

    public class TerryTTTHandsOff : Dialogue {
        public TerryTTTHandsOff() {
            Say("Hey!");
            Say("Hands off of my valuable product");
        }
    }

    public class TerryTTTDescription : Dialogue {
        public TerryTTTDescription() {
            Say("This is 'Terry The Terrible Torture Turtle'");
            Say("Not to be cofused with 'Terry The Terrible Torture Tortoise'");
            Say("Although they might look similar one of them is much more dangerous");
            Say("But I forgot which one...")
            .DoAfter(new TriggerDialogueAction<TerryTTTChoice>());
        }
    }

    public class TerryTTTChoice : Dialogue {
        public TerryTTTChoice() {
            TerryTTT t = TerryTTT.t;

            Say("This one can be bought for 300 coins")
            .Choice(new TextOption("I don't think I can use that..."))
            .Choice(new ItemOption(t.horrorcoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTMoney>()))
            .Choice(new ItemOption(t.startcoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTMoney>()))
            .Choice(new ItemOption(t.cutecoin)
                .IfChosen(new TriggerDialogueAction<TerryTTTMoney>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<TerryTTTUseless>()));
        }
    }

    public class TerryTTTMoney : Dialogue {
        public TerryTTTMoney() {
            Say("Hm you can't afford Terry with just that")
            .DoAfter(new TriggerDialogueAction<TerryTTTChoice>());
        }
    }

    public class TerryTTTUseless : Dialogue {
        public TerryTTTUseless() {
            Say("You can't pay with that, sorry")
            .DoAfter(new TriggerDialogueAction<TerryTTTChoice>());
        }
    }
}
