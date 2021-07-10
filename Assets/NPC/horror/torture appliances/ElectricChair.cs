using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricChair : DialogueTrigger
{
    public Item _given_horrorcoin;
    public Item horrorcoin;
    public Item startcoin;
    public Item cutecoin;

    public const string DefName = "Steve E Horror";
    public const string UwuName = "Steve E Howwow";

    public static ElectricChair e;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        ElectricChair.e = this;

        if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            name = DefName;
            return new ElectricChairDescription();
        }
        name = UwuName;
        return new ElectricChairHandsOff();
    }

    public class ElectricChairDescription : Dialogue {
        public ElectricChairDescription() {
            Say("The newest addition to my collection");
            Say("the electric chair!");
            Say("It is super fun and quite comfy");
            Say("Why don't you try it out, I am sure you won't regret it!")
            .Choice(new TextOption("no thank you"))
            .Choice(new TextOption("maybe later..."));
            Say("Oh, I almost forgot:");
            Say("If you use It make sure to stay a bit back");
            Say("Or don't If that is what you're after")
           .DoAfter(new TriggerDialogueAction<ElectricChairChoice>());
        }
    }

    public class ElectricChairChoice : Dialogue {
        public ElectricChairChoice() {
            ElectricChair e = ElectricChair.e;

            Say("But enough talking, do you want to buy it?");
            Say("It is on sale just today for an incredilble price of just 7000c")
            .Choice(new TextOption("I don't need a new chair"))
            .Choice(new ItemOption(e.horrorcoin)
                .IfChosen(new TriggerDialogueAction<ElectricChairCoin>()))
            .Choice(new ItemOption(e.cutecoin)
                .IfChosen(new TriggerDialogueAction<ElectricChairCoin>()))
            .Choice(new ItemOption(e.startcoin)
                .IfChosen(new TriggerDialogueAction<ElectricChairCoin>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<ElectricChairItem>()));
        }
    }

    public class ElectricChairCoin : Dialogue {
        public ElectricChairCoin() {
            Say("With that amount you couldnt even pay the electricity bill");
        }
    }

    public class ElectricChairItem : Dialogue {
        public ElectricChairItem() {
            Say("This isn't gonna work...");
        }
    }

    public class ElectricChairHandsOff : Dialogue { 
        public ElectricChairHandsOff() {
            Say(Uwu.Uwufy("Get away from that!"));
            Say(Uwu.Uwufy("These are dangerous instruments!"));
        }
    }
}
