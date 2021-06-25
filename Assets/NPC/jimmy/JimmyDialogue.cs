using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : DialogueTrigger
{
    public Portal portalToMiniGame;
    public Item bucket;
    public Item empty;


    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(bucket)) {
            return new JimmyWinDialogue();
        }
        else if (Inventory.Instance.HasItem(empty)){
            return new JimmyLoseDialogue();
        }
        return new JimmyDefaultDialogue();
    }
}

public class JimmyDefaultDialogue : Dialogue {
    public JimmyDefaultDialogue() {

        JimmyDialogue diaTrigger = (JimmyDialogue) JimmyDialogue.Instance;

        Say("Pipes are busted, can ya help?")
            .Choice(
                new TextOption("Yes")
                .IfChosen(new DialogueAction(() => {
                    diaTrigger.EnterMiniGame();
                }))
            )
            .Choice(new TextOption("No"));

    }
}


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

        Say("Thank for the help!");
    }
}


public class JimmyLoseDialogue : Dialogue {
    public JimmyLoseDialogue() {

        JimmyDialogue diaTrigger = (JimmyDialogue) JimmyDialogue.Instance;

        Say("Well, that didn't work out too well, did it?");

        Say("Try again?")
            .Choice(
                new TextOption("Yes")
                .IfChosen(new DialogueAction(() => {
                    diaTrigger.EnterMiniGame();
                }))
            )
            .Choice(new TextOption("Later"));
    }
}