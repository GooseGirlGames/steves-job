using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : DialogueTrigger
{
    public Portal portalToMiniGame;
    public Item shirt;
    public Item clean_dress;
    public Item dirty_dress;
    public Item dirty_shirt;
    public Item bucket;
    public Item empty;
    public Item bucket_empty_cute;
    public Item bucket_full_cute;
    public Item gameLost;
    public Item gamePlayed;
    public Item goose_clean;
    public Item goose_bloody;
    public Item goose_bloody_bow;
    public Item goose_bow;
    public Item cutecoin;
    public Item horrorcoin;
    public Item startcoin;

    public Item switch_broken_cute;
    public Item switch_broken;
    public Item switch_broken_horror;
    public static JimmyDialogue t = null;


    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue() {
        t = this;
        if (Inventory.Instance.HasItem(gamePlayed)) {
            return new NoBigAnswer();
        }
        else if (Inventory.Instance.HasItem(gameLost)){
            return new JimmyLoseDialogue();
        }
        if (Inventory.Instance.HasItem(bucket)) {
            return new JimmyWinDialogue();
        }
        else if (Inventory.Instance.HasItem(empty) || Inventory.Instance.HasItem(bucket_empty_cute)){
            return new JimmyHasBucketDialogue();
        }
        return new JimmyDefaultDialogue();
    }

    public class JimmyDefaultDialogue : Dialogue {
        public JimmyDefaultDialogue() {
            Say("Hey there, Janitor!");
            Say("Pipes are busted, can ya help?")
                .Choice(new TextOption("Yes")
                    .IfChosen(new TriggerDialogueAction<BucketMissing>()))
                .Choice(new TextOption("No"));
        }
    }

    public class JimmyHasBucketDialogue : Dialogue {
        public JimmyHasBucketDialogue() {
            Say("Hey there, Janitor!");
            Say("Pipes are busted, can ya help?")
                .Choice(
                    new TextOption("Yes")
                    .IfChosen(new DialogueAction(() => {
                        t.EnterMiniGame();
                    }))
                )
                .Choice(new TextOption("No"));
        }
    }

    public class BucketMissing : Dialogue {
        public BucketMissing() {
            Say("ummm... I think you need a bucket for this.");
            Say("You should look for one.");
        }
    }

    public class CantDoAnythingWithIt : Dialogue {
        public CantDoAnythingWithIt() {
            Say("uhhh... this... uhh... well that is awkward.");
            Say("I just don't know what to do with it.")
                .Choice(new TextOption("..."));
        }
    }
    /*----------------------- Game has been won --------------------------------------------*/
    public class NoBigAnswer : Dialogue {
        public NoBigAnswer() {
            Say("What's your problem?");
            Say("You have something to clean?")
            .Choice(new TextOption("Later"))
            // Buckets
            .Choice(new ItemOption(t.empty)
                .IfChosen(GiveItem(t.bucket))
                .IfChosen(new TriggerDialogueAction<BucketFilledUp>()))
            .Choice(new ItemOption(t.bucket)
                .IfChosen(new TriggerDialogueAction<NothingToDoHere>()))
            .Choice(new ItemOption(t.bucket_empty_cute)
                .IfChosen(GiveItem(t.bucket_full_cute))
                .IfChosen(new TriggerDialogueAction<BucketFilledUp>()))
            .Choice(new ItemOption(t.bucket_full_cute)
                .IfChosen(new TriggerDialogueAction<NothingToDoHere>()))
            // Shirts
            .Choice(new ItemOption(t.clean_dress)
                .IfChosen(new TriggerDialogueAction<CleanDressDia>()))
            .Choice(new ItemOption(t.shirt)
                .IfChosen(new TriggerDialogueAction<ShirtDia>()))
            .Choice(new ItemOption(t.dirty_shirt)
                .IfChosen(new TriggerDialogueAction<DirtyShirtDia>()))
            // Geese
            .Choice(new ItemOption(t.goose_clean)
                .IfChosen(new TriggerDialogueAction<GooseDia>()))
            .Choice(new ItemOption(t.goose_bow)
                .IfChosen(new TriggerDialogueAction<GooseBowDia>()))
            // Coins
            .Choice(new ItemOption(t.cutecoin)
                .IfChosen(new TriggerDialogueAction<CuteCoinDia>()))
            .Choice(new ItemOption(t.startcoin)
                .IfChosen(new TriggerDialogueAction<StartCoinDia>()))
            // Switches
            .Choice(new ItemOption(t.switch_broken)
                .IfChosen(new TriggerDialogueAction<BloodifySwitch>()))
            .Choice(new ItemOption(t.switch_broken_cute)
                .IfChosen(new TriggerDialogueAction<BloodifySwitch>()))
            // Others
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<CantDoAnythingWithIt>()));  
        }
    }
    /*----------------------- Shirt Dialogue --------------------------------------------*/
    public class ShirtDia : Dialogue {
        public ShirtDia() {
            Say("Urgh");
            Say("This looks disgusting!!!");
            Say("Let me clean that for you.")
                .DoAfter(GiveItem(t.dirty_shirt));
        }
    }
    public class CleanDressDia : Dialogue {
        public CleanDressDia() {
            Say("Urgh");
            Say("This looks disgusting!!!");
            Say("Let me clean that for you.")
                .DoAfter(GiveItem(t.dirty_dress));
        }
    }

    public class GooseDia : Dialogue {
        public GooseDia() {
            Say("Urgh");
            Say("This looks disgoosting!!!");
            Say("Let me clean that for you.")
                .DoAfter(GiveItem(t.goose_bloody));
        }
    }

    public class GooseBowDia : Dialogue {
        public GooseBowDia() {
            Say("Yikes");
            Say("This looks disgoosting!!!");
            Say("Let me clean that for you.")
                .DoAfter(GiveItem(t.goose_bloody_bow));
        }
    }

    public class CuteCoinDia : Dialogue {
        public CuteCoinDia() {
            Say("Urgh");
            Say("This looks disgusting!!!");
            Say("Don't mind me laundering that money for ya!")
                .DoAfter(RemoveItem(t.cutecoin))
                .DoAfter(GiveItem(t.horrorcoin));
        }
    }

    public class StartCoinDia : Dialogue {
        public StartCoinDia() {
            Say("Urgh");
            Say("This looks disgusting!!!");
            Say("Don't mind me laundering that money for ya!")
                .DoAfter(RemoveItem(t.startcoin))
                .DoAfter(GiveItem(t.horrorcoin));
        }
    }

    public class BloodifySwitch : Dialogue {
        public BloodifySwitch() {
            Say("Oof");
            Say("These parts look disgusting!!!");
            Say("There you go, much better now.")
                .DoAfter(GiveItem(t.switch_broken_horror));
        }
    }

    public class DirtyShirtDia : Dialogue {
        public DirtyShirtDia() {
            Say("Looks completely fine to me.");
            Say("I don't know what you want me to do with that.")
                .Choice(new TextOption("..."));
        }
    }

    
    /*----------------------- Bucket Dialogue ----------------------------------------------------------*/
    public class BucketFilledUp : Dialogue {
        public BucketFilledUp() {
            Say("There you go.");
            Say("Your bucket is all filled up again.")
            .Choice(new TextOption("..."));
        }
    }
    public class NothingToDoHere : Dialogue{
        public NothingToDoHere() {
            Say("Huh...?");
            Say("All good with that?");
            Say("Give me something to work with.")
            .Choice(new TextOption("..."));
        }
    }
    /*----------------------- Game still needs to be played --------------------------------------------*/
    public class JimmyWinDialogue : Dialogue {
        public JimmyWinDialogue() {
            Say("Excellent!")
                .If(() => 
                    BloodFalling.splatCount == 0
                );

            Say("Well, that was close, but good enough...")
                .If(() => 
                    BloodFalling.splatCount == 2
                );

            Say("Thanks for the help!")
                .Do(GiveItem(t.gamePlayed));

            Say("You can keep the bucket, and here, have some of my spare change.")
                .Do(GiveItem(t.horrorcoin));
        }
    }



    public class JimmyLoseDialogue : Dialogue {
        public JimmyLoseDialogue() {

            

            Say("Well, that didn't work out too well, did it?");

            Say("Try again?")
                .Choice(
                    new TextOption("Yes")
                    .IfChosen(new DialogueAction(() => {
                        t.EnterMiniGame();
                    })))
                .Choice(new TextOption("Later"));
        }
    }


}