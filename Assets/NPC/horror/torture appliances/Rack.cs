using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : DialogueTrigger
{
    public Item _given_horrorcoin;
    public Item horrorcoin;
    public Item startcoin;
    public Item cutecoin;
    
    public Sprite uwu_ava;
    public Sprite normal_ava;

    public const string DefName = "Steve E Horror";
    public const string UwuName = "Steve E Howwow";

    public static Rack r;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        Rack.r = this;

        if (Inventory.Instance.HasItem(_given_horrorcoin)) {
            name = DefName;
            avatar = normal_ava;
            return new RackDescription();
        }
        name = UwuName;
        avatar = uwu_ava;
        return new RackHandsOff();
    }

    public class RackDescription : Dialogue {
        public RackDescription() {
            Say("This is another top-seller, an original torture rack from the middle ages");
            Say("It is not only effective for torturing other people;");
            Say("You can also use it yourself to reach that magical 6\" mark");
            Say("The design has barely changed over the centuries");
            Say("A true testament to its excellent architecture");
            Say("and it is still in pristine condition")
            .DoAfter(new TriggerDialogueAction<RackChoice>());
        }
    }

    public class RackChoice : Dialogue {
        public RackChoice() {
            Rack r = Rack.r;

            Say("Since we are good friends I can make you an offer: For only 1000c it could be yours!")
            .Choice(new TextOption("Um I'm good, thanks"))
            .Choice(new ItemOption(r.horrorcoin)
                .IfChosen(new TriggerDialogueAction<RackCoin>()))
            .Choice(new ItemOption(r.startcoin)
                .IfChosen(new TriggerDialogueAction<RackCoin>()))
            .Choice(new ItemOption(r.cutecoin)
                .IfChosen(new TriggerDialogueAction<RackCoin>()))
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<RackItem>()));
        }
    }

    public class RackCoin : Dialogue {
        public RackCoin() {
            Say("Hm");
            Say("We might be friends but if you only offer this much I'd be basically gifting it to you");
            Say("This might be fine with something cheaper, but I have to get by somehow");
        }
    }

    public class RackItem : Dialogue {
        public RackItem() {
            Say("That's not valid currency, is it?!");
        }
    }

    public class RackHandsOff :  Dialogue {
        public RackHandsOff() {
            Say(Uwu.Uwufy("This cost more than you can imagine!"));
            Say(Uwu.Uwufy("Keep your dirty hands off of it!"));
        }
    }
}
