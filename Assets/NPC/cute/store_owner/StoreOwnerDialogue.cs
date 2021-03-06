using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOwnerDialogue : DialogueTrigger {
    public Portal portalToMiniGame;
    new public static StoreOwnerDialogue Instance = null;
    public Item crank;
    public Item _storeowner_later;
    public Item _racoonMad;
    public Item _miniRacoonGamePlayed;
    public Item _miniRacoonGameWon;
    public Item _restoredCandyman;
    public Animator animator;
    public Sprite avatar_happy;
    public Sprite avatar_sad;
    public Sprite sad_storeowner;
    public Sprite happy_storeowner;
    public Sprite storeowner_wo_cane;
    public Transform hint_happy;
    public Transform hint_sad;


    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    public override Dialogue GetActiveDialogue(){
        UpdateAnimator();
        if(Inventory.Instance.HasItem(_storeowner_later)) {
            return new CameBackDia();
        }
        if(Inventory.Instance.HasItem(_racoonMad)){
            return new StoreOwnerDefaultDialogue();
        }
        if(Inventory.Instance.HasItem(_miniRacoonGameWon)){
            return new ThankYouDia();
        }
        if(Inventory.Instance.HasItem(_miniRacoonGamePlayed)){
            return new PlayMiniGameAgain();
        }
        return new HelloIAmStoreOwnerDia();
    }

    public void UpdateAnimator() {
        avatar = avatar_happy;
        hintPosition = hint_happy;
        if (Inventory.Instance.HasItem(_racoonMad) || Inventory.Instance.HasItem(_miniRacoonGamePlayed)) {
            animator.SetTrigger("GetSad");
            avatar = avatar_sad;
            hintPosition = hint_sad;
        }
        if (Inventory.Instance.HasItem(_restoredCandyman)) {
            animator.SetTrigger("RemoveCane");
            avatar = avatar_happy;
            hintPosition = hint_happy;
        }
    }

    void Awake() {
        UpdateAnimator();
        Instance = this;
    }


    public class HelloIAmStoreOwnerDia : Dialogue {
        public HelloIAmStoreOwnerDia(){
            Say("Hello, nice to meet you!");
            Say("I am the sweetest owner of the sweetest store to ever exist.");
            Say("My store is like my child.");
            Say(
                "I would be the saddest but still the sweetest store owner to ever exist if "
                + "anything ever happened to my delicious child!");
            Say("Well then, it was nice meeting you, dear janitor!");
        }
    }

    public class ThankYouDia : Dialogue {
        public ThankYouDia(){
            Say("*cries*");
            Say("thank you so sooo soooo much!!!");
            Say("I thought i had lost my child forever!");

            Say(
                "I can't pay you with much, but maybe this fine walking " + 
                "cane might bring you some joy."
            )
            .If(DoesNotHaveItem(StoreOwnerDialogue.Instance._restoredCandyman))
            .Do(GiveItem(StoreOwnerDialogue.Instance.crank))
            .Do(GiveItem(StoreOwnerDialogue.Instance._restoredCandyman));
        }
    }

    public class PlayMiniGameAgain : Dialogue {
        public PlayMiniGameAgain(){
            Say("...you promised you'dl help...");
            Say("...please just save the kid!")
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
            Say("This racoon... he... he was on a strict diet... he he...");
            Say("This idiot must've gotten his little cute tiny paws on some candy.");
            Say(
                "...he is not good with sugar... " + 
                "so I told him that he is not allowed to eat candy ever again!"
            );
            Say("*cries*");
            Say("he just came into my store with his mouth full of the sticky evidence.");
            Say("he was shaking and his eyes full of rage... he suddenly got into a huge sugar rush.");
            Say("He said: \"Give me some more candy\" with this scary voice and I said \"no\" to him.");
            Say("I've never seen someone this angry and fled while my store is getting completely destroyed.");
            Say("...say, could you please help me to stop him?")
                .Choice(
                    new TextOption("of course")
                        .IfChosen(new DialogueAction(() => {
                            StoreOwnerDialogue.Instance.EnterMiniGame();
                    }))
                    //.IfChosen(new TriggerDialogueAction<WarningDia>())
                )
                .Choice(
                    new TextOption("wait... what?")
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
                    new TextOption("...I guess")
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
                    new TextOption("...cringe")
                    .IfChosen(GiveItem(StoreOwnerDialogue.Instance._storeowner_later))
            );
        }
    }

}