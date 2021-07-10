using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOwnerDialogue : DialogueTrigger {
    public Portal portalToMiniGame;
    new public static StoreOwnerDialogue Instance = null;
    public Item _storeowner_later;
    public Item _racoonMad;
    public Item _miniRacoonGamePlayed;
    public Item _restoredCandyman;

    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue(){
        if(Inventory.Instance.HasItem(_storeowner_later)) {
            return new CameBackDia();
        }
        if(Inventory.Instance.HasItem(_racoonMad)){
            return new StoreOwnerDefaultDialogue();
        }
        if(Inventory.Instance.HasItem(_restoredCandyman)){
            return new ThankYouDia();
        }
        if(Inventory.Instance.HasItem(_miniRacoonGamePlayed)){
            return new PlayMiniGameAgain();
        }
        return new HelloIAmStoreOwnerDia();
    }

    void Awake()
    {
        Instance = this;
    }
}

public class HelloIAmStoreOwnerDia : Dialogue {
    public HelloIAmStoreOwnerDia(){
        Say("Hello, nice to meet you!");
        Say("I am the sweetest owner of the sweetest store to ever exist");
        Say("My store is like my child");
        Say("I would be the saddest but still the sweetest store owner to ever exist if anything ever happens to my delicious child!");
        Say("Well then, it was nice meeting you, dear janitor!");
    }
}

public class ThankYouDia : Dialogue {
    public ThankYouDia(){
        Say("*cries*");
        Say("thank you so sooo soooo much!!!");
        Say("I thought i had lost my child forever!");
    }
}

public class PlayMiniGameAgain : Dialogue {
    public PlayMiniGameAgain(){
        Say("..you promised you'll help");
        Say("..please just save the kid!")
            .Choice(
                new TextOption("okay")
                .IfChosen(new DialogueAction(() => {
                    StoreOwnerDialogue.Instance.EnterMiniGame();
            })))
            .Choice(
                new TextOption("later")
                .IfChosen(new TriggerDialogueAction<CriesDia>())
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance._storeowner_later))
            );
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
                    .IfChosen(new DialogueAction(() => {
                        StoreOwnerDialogue.Instance.EnterMiniGame();
                }))
                //.IfChosen(new TriggerDialogueAction<WarningDia>())
            )
            .Choice(
                new TextOption("wait..what?")
                .IfChosen(new TriggerDialogueAction<RacoonStoryDia>())
            )
            .Choice(
                new TextOption("later")
                .IfChosen(new TriggerDialogueAction<CriesDia>())
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance._storeowner_later))
            );
        
    }
}
public class CameBackDia : Dialogue {
    public CameBackDia() {
        Say("YOU CAME BACK! Will you help?")
            .Choice(
                new TextOption("..I guess")
                .IfChosen(new DialogueAction(() => {
                    StoreOwnerDialogue.Instance.EnterMiniGame();
                }))) 
                //.IfChosen(new TriggerDialogueAction<WarningDia>()))
            .Choice(
                new TextOption("maybe later")
                .IfChosen(new TriggerDialogueAction<CriesDia>())
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance._storeowner_later))
            );  
    }   
}
public class CriesDia : Dialogue {
    public CriesDia() {
        Say("*cries*")
            .Choice(
                new TextOption("..cringe")
                .IfChosen(GiveItem(StoreOwnerDialogue.Instance._storeowner_later))
        );
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