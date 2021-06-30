using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : DialogueTrigger
{
    public Portal portalToMiniGame;
    public Item shirt;
    public Item clean_shirt;
    public Item dirty_shirt;
    public Item bucket;
    public Item empty;
    public Item gameLost;
    public Item gamePlayed;
    public static new JimmyDialogue Instance = null;


    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(gamePlayed)) {
            return new NoBigAnswer();
        }
        else if (Inventory.Instance.HasItem(gameLost)){
            return new JimmyLoseDialogue();
        }
        if (Inventory.Instance.HasItem(bucket)) {
            return new JimmyWinDialogue();
        }
        else if (Inventory.Instance.HasItem(empty)){
            return new JimmyHasBucketDialogue();
        }
        return new JimmyDefaultDialogue();
    }

    private void Awake() {
        Instance = this;
    }
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
                    JimmyDialogue.Instance.EnterMiniGame();
                }))
            )
            .Choice(new TextOption("No"));
    }
}

public class BucketMissing : Dialogue {
    public BucketMissing() {
        Say("ummm..I think you need a bucket for this");
        Say("You should look for one");
    }
}

public class CantDoAnythingWithIt : Dialogue {
    public CantDoAnythingWithIt() {
        Say("uhhh...this...uhh..well that is awkward");
        Say("I just dont know what to do with it")
            .Choice(new TextOption("..."));
    }
}
/*----------------------- Game has been won --------------------------------------------*/
public class NoBigAnswer : Dialogue {
    public NoBigAnswer() {
        Say("What's your problem?");
        Say("You have something to clean?")
        .Choice(new TextOption("..."))
        .Choice(new ItemOption(JimmyDialogue.Instance.empty)
            .IfChosen(GiveItem(JimmyDialogue.Instance.bucket))
            .IfChosen(new TriggerDialogueAction<BucketFilledUp>()))
        .Choice(new ItemOption(JimmyDialogue.Instance.bucket)
            .IfChosen(new TriggerDialogueAction<NothingToDoHere>()))
        .Choice(new ItemOption(JimmyDialogue.Instance.clean_shirt)
            .IfChosen(new TriggerDialogueAction<CleanShirtDia>()))
        .Choice(new ItemOption(JimmyDialogue.Instance.shirt)
            .IfChosen(new TriggerDialogueAction<CleanShirtDia>()))
        .Choice(new ItemOption(JimmyDialogue.Instance.dirty_shirt)
            .IfChosen(new TriggerDialogueAction<DirtyShirtDia>()))
        .Choice(new OtherItemOption()
            .IfChosen(new TriggerDialogueAction<CantDoAnythingWithIt>()));  
    }
}
/*----------------------- Shirt Dialogue --------------------------------------------*/
public class CleanShirtDia : Dialogue {
    public CleanShirtDia() {
        Say("urgh");
        Say("this looks disgusting!!!");
        Say("Let me clean that for you")
            .DoAfter(GiveItem(JimmyDialogue.Instance.dirty_shirt));
    }
}
public class DirtyShirtDia : Dialogue {
    public DirtyShirtDia() {
        Say("looks completely fine to me");
        Say("I dont know what you want me to do with that")
            .Choice(new TextOption("..."));
    }
}
/*----------------------- Bucket Dialogue ----------------------------------------------------------*/
public class BucketFilledUp : Dialogue {
    public BucketFilledUp() {
        Say("There you go");
        Say("Your bucket is all filled up again")
        .Choice(new TextOption("..."));
    }
}
public class NothingToDoHere : Dialogue{
    public NothingToDoHere() {
        Say("Huh...?");
        Say("All good with that?");
        Say("Give me something to work with")
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

        Say("Thank for the help!")
            .Do(GiveItem(JimmyDialogue.Instance.gamePlayed));
    }
}



public class JimmyLoseDialogue : Dialogue {
    public JimmyLoseDialogue() {

        

        Say("Well, that didn't work out too well, did it?");

        Say("Try again?")
            .Choice(
                new TextOption("Yes")
                .IfChosen(new DialogueAction(() => {
                    JimmyDialogue.Instance.EnterMiniGame();
                })))
            .Choice(new TextOption("Later"));
    }
}
