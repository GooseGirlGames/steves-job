using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonCuteDialogue :  DialogueTrigger{
    public static RacoonCuteDialogue t = null;
    public Item snack;
    public Item coin;
    public Item crank;
    public Item _miniRacoonGamePlayed;
    public Item _miniRacoonGameWon;
    public Item _racoonMad;
    public GameObject storeOwner;
    public Sprite sadStoreOwner;
    public Sprite neutralStoreOwner;


    public override Dialogue GetActiveDialogue(){

        if(Inventory.Instance.HasItem(_miniRacoonGamePlayed)){
            RacoonCuteDialogue.t.storeOwner.GetComponent<SpriteRenderer>().sprite = RacoonCuteDialogue.t.sadStoreOwner;
            RacoonCuteDialogue.t.transform.position = new Vector3(8.4f,-2.2f,1f);
            return new MiniGameLostDia();
        } 
        if(Inventory.Instance.HasItem(_miniRacoonGameWon)){
            return new MiniGameFinishedDia();
        } 
        return new RacoonCuteDefaultDialogue();
    }

    void Awake(){
        t = this;
    }

    
}
public class RacoonCuteDefaultDialogue : Dialogue {
    public RacoonCuteDefaultDialogue(){
        Say("*sniff...sniff...*");
        Say("*puppy eyes* Oh hello mr. janitor.");
        Say("I have a big big big biiiiig problem, can you please help me out?");
        Say("I have also a little compensation for your kind service sir")
            .DoAfter(new TriggerDialogueAction<ChoiceDialogue>());
            

            
    }
}

public class MiniGameLostDia : Dialogue {
    public MiniGameLostDia(){
        Say("LOSER!..");
    }
}
public class ChoiceDialogue : Dialogue {
    public ChoiceDialogue(){
        Say("I feel sick and I need candy as my medicine")
            .Choice(
                new TextOption("You don't look sick to me")
                .IfChosen(new TriggerDialogueAction<SickRacoonDia>()))
            .Choice(
                new ItemOption(RacoonCuteDialogue.t.snack)
                .IfChosen(new TriggerDialogueAction<SnackDialogue>()))
            .Choice(
                new TextOption("no!"));    
    }
}


public class MiniGameFinishedDia : Dialogue {
    public MiniGameFinishedDia(){
        Say("Oh this is awkward...");
        Say("I am sorry..but anyways thank you for giving me a treat :3")
            .DoAfter(new DialogueAction(()=> {
                RacoonCuteDialogue.t.storeOwner.GetComponent<SpriteRenderer>().sprite = RacoonCuteDialogue.t.neutralStoreOwner;
            }))
            .DoAfter(RemoveItem(RacoonCuteDialogue.t._racoonMad))
            .DoAfter(RemoveItem(RacoonCuteDialogue.t._miniRacoonGamePlayed));
        
    }
}
public class SnackDialogue : Dialogue {
    public SnackDialogue(){
        Say("Thank you sooooo much my dear janitor")
            .DoAfter(RemoveItem(RacoonCuteDialogue.t.snack));
        Say("Since you helped me I will gift you this beautiful coin!")
            .DoAfter(GiveItem(RacoonCuteDialogue.t.coin))
            .DoAfter(GiveItem(RacoonCuteDialogue.t._racoonMad))
            .DoAfter(new DialogueAction(()=> {
                RacoonCuteDialogue.t.storeOwner.GetComponent<SpriteRenderer>().sprite = RacoonCuteDialogue.t.sadStoreOwner;
            }))
            .DoAfter(new DialogueAction(()=> {
                RacoonCuteDialogue.t.transform.position = new Vector3(8.4f,-2.2f,1f);
            }));

    }
}
public class SickRacoonDia : Dialogue {
    public SickRacoonDia(){
        Say("i swear, i really need some of that sweet medicine...and by that I mean candy");
        Say("dont you want my little suprise that I have for you?")
            .DoAfter(new TriggerDialogueAction<RacoonCuteDefaultDialogue>());
    }
}