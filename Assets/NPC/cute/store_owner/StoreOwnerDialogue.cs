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
        Say("he was on a strict diet..he he..");
        Say("This idiot must've got some candy");
        Say("..he is not good with sugar..so I told him that he is not allowed to eat sugar ever again!");
        Say("*cries*");
        Say("he just came into my store with mouth full of the sticky evidence");
        Say("he was shaking and his eyes full of rage...he suddenly got into a huge sugar rush");
        Say("He said: ''give me some more candy'' with this scary voice");
        Say("I said ''no'' to him");
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
        .Choice(new TextOption("Ah, think I'll pass"));
    }
}