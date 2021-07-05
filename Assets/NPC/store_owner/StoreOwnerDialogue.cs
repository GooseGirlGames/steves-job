using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOwnerDialogue : DialogueTrigger {
    public Portal portalToMiniGame;
    new public static StoreOwnerDialogue Instance = null;
    public Item not_active;

    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue(){
        if(Inventory.Instance.HasItem(not_active)) {
            return new CameBackDia();
        }
        return new StoreOwnerDefaultDialogue();
    }

    void Awake()
    {
        Instance = this;
    }
}


public class StoreOwnerDefaultDialogue : Dialogue {
    public StoreOwnerDefaultDialogue(){
        Say("*cries*")
            .Choice(
                new TextOption("Are you okay?")
                .IfChosen(new TriggerDialogueAction<NotOkayDia>())
            )
            .Choice(
                new TextOption("what happened?")
                .IfChosen(new TriggerDialogueAction<RacoonStoryDia>())
            );
    }
}

public class NotOkayDia : Dialogue {
    public NotOkayDia(){
        Say("*cries*")
            .Choice(
                new TextOption("what is wrong!")
                .IfChosen(new TriggerDialogueAction<RacoonStoryDia>())
            )
            .Choice(
                new TextOption("leave him be")
            );
    }
}

public class RacoonStoryDia : Dialogue {
    public RacoonStoryDia(){
        Say("This racoon..");
        Say("he...he..");
        Say("A racoon asked for cotton candy and I told him there was one package of it left");
        Say("This idiot bought it and...");
        Say("..and then said he needed to use the restroom");
        Say("*cries*");
        Say("All was good but then he returned...trembling with anger");
        Say("I looked at his hands and there it was..");
        Say("the sticky evidence of him washing his cotton candy");
        Say("He said: ''My cotton candy vanished...give me some more''");
        Say("I said to him: ''like I told you, I sold the last package to you''");
        Say("I've never seen someone this angry and fled..");
        Say("All i could hear is getting my store completely destroyed");
        Say("...say, could you please help me to stop him?")
            .Choice(
                new TextOption("of course")
                    /*.IfChosen(new DialogueAction(() => {
                        StoreOwnerDialogue.Instance.EnterMiniGame();
                })) */
                .IfChosen(new TriggerDialogueAction<WarningDia>())
            )
            .Choice(
                new TextOption("later")
                .IfChosen(new TriggerDialogueAction<CriesDia>())
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance.not_active))
                );
        
    }
}
public class CameBackDia : Dialogue {
    public CameBackDia() {
        Say("YOU CAME BACK! Will you help?")
            .Choice(
                new TextOption("..I guess")
                /*.IfChosen(new DialogueAction(() => {
                    StoreOwnerDialogue.Instance.EnterMiniGame();
                }))) */
                .IfChosen(new TriggerDialogueAction<WarningDia>()))
            .Choice(
                new TextOption("maybe later")
                .IfChosen(new TriggerDialogueAction<CriesDia>())
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance.not_active))
            );  
    }   
}
public class CriesDia : Dialogue {
    public CriesDia() {
        Say("*cries*");
    }
}

//temporary as the minigame isn't finished yet

public class WarningDia : Dialogue {
    public WarningDia() {
        Say("Be careful, there are many dangers such as unfinished minigames up ahead");
        Say("Are you sure you want to continue")
        .Choice(
            new TextOption("Can't hurt to try, right?")
            .IfChosen(new DialogueAction(() => {
                    StoreOwnerDialogue.Instance.EnterMiniGame();
            })))
        .Choice(new TextOption("Ah, think I'l pass"));
    }
}