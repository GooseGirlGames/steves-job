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
    public Item _restored_candyman;
    public Item _racoonMad;
    public Item _alreadyTalked;
    public Sprite ava_sleepy;
    public Sprite ava_angy;
    public Sprite ava_normal;
    private Animator animatior;
    public SpriteRenderer windowRaccoon;
    public StoreOwnerDialogue storeowner;

    private const string LOCK_TAG = "Cute Raccoon";
    public Animator fadeToBlack;

    public void UpdateState() {
        if (Inventory.Instance.HasItem(_restored_candyman)
            || Inventory.Instance.HasItem(_miniRacoonGameWon)) {
            // sleepy
            avatar = ava_sleepy;
            animatior.SetBool("Sleepy", true);
            GetComponent<SpriteRenderer>().enabled = true;
            windowRaccoon.enabled = false;
        } else if (Inventory.Instance.HasItem(_racoonMad)) {
            // angy
            avatar = ava_angy;
            GetComponent<SpriteRenderer>().enabled = false;
            windowRaccoon.enabled = true;
        } else {
            // normal
            avatar = ava_normal;
            animatior.SetBool("Sleepy", false);
            GetComponent<SpriteRenderer>().enabled = true;
            windowRaccoon.enabled = false;
        }
        storeowner.UpdateAnimator();
    }

    public override Dialogue GetActiveDialogue(){
        UpdateState();

        if(Inventory.Instance.HasItem(_restored_candyman)){
            return new MiniGameFinishedDia();
        } 
        if(Inventory.Instance.HasItem(_racoonMad)){
            return new DestructionNoise();
        }
        if(Inventory.Instance.HasItem(_miniRacoonGamePlayed)){
            return new MiniGameLostDia();
        }
        if(Inventory.Instance.HasItem(_alreadyTalked)){
            return new NewChoiceDialogue();
        }
        
        return new RacoonCuteDefaultDialogue();
    }

    void Awake(){
        t = this;
        animatior = GetComponent<Animator>();
        UpdateState();
    }

    public void FadeOut() {
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        player.Lock(LOCK_TAG);
        fadeToBlack.SetFloat("Speed", 1.8f);
        fadeToBlack.SetTrigger("ExitScene");
    }
    public void FadeIn() {
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        player.Unlock(LOCK_TAG);
        fadeToBlack.SetTrigger("EnterScene");
        fadeToBlack.SetFloat("Speed", 1);
    }

    
}
public class RacoonCuteDefaultDialogue : Dialogue {
    public RacoonCuteDefaultDialogue(){
        Say("*sniff...sniff...*");
        Say("*puppy eyes* Oh hello, Mr. Janitor.");
        Say("I have a big big big biiiiig problem, can you please help me out?");
        Say("I have also a little compensation for your kind service sir")
            .DoAfter(new TriggerDialogueAction<ChoiceDialogue>());
            

            
    }
}

public class MiniGameLostDia : Dialogue {
    public MiniGameLostDia(){
        Say("LOSER!");
    }
}

public class DestructionNoise : Dialogue {
    public DestructionNoise(){
        Say("*destruction noises*");
    }
}

public class ChoiceDialogue : Dialogue {
    public ChoiceDialogue(){
        Say("I feel sick and I need candy as my medicine.")
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

public class NewChoiceDialogue : Dialogue {
    public NewChoiceDialogue(){
        Say("Please, I need my medicine.")
            .Choice(
                new ItemOption(RacoonCuteDialogue.t.snack)
                .IfChosen(new TriggerDialogueAction<SnackDialogue>()))
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<WrongSnack>())
            )
            .Choice(
                new TextOption("no!")
            );    
    }
}

public class WrongSnack : Dialogue {
    public WrongSnack() {
        string item = DialogueManager.Instance.currentItem.name;
        Say("Dude, I need something way sweeter to snack than a " + item + "... Please!")
        .DoAfter(new TriggerDialogueAction<NewChoiceDialogue>());
    }
}


public class MiniGameFinishedDia : Dialogue {
    public MiniGameFinishedDia(){
        Say("oof that was a workout... *yawn*");
        Say("Oh this is awkward...");
        Say("well I should go to bed!");
        Say("Anyways, I am sorry... but thank you for giving me a treat :3")
            .DoAfter(RemoveItem(RacoonCuteDialogue.t._racoonMad))
            .DoAfter(RemoveItem(RacoonCuteDialogue.t._miniRacoonGamePlayed));
        
    }
}
public class SnackDialogue : Dialogue {
    public SnackDialogue(){
        Say("Thank you sooooo much my dear janitor")
            .Do(RemoveItem(RacoonCuteDialogue.t.snack));
        Say("Since you helped me I will gift you this beautiful coin!")
            .Do(GiveItem(RacoonCuteDialogue.t.coin));
        Say("Aaaaaa... I feel *alive* again!")
        .DoAfter(RacoonCuteDialogue.t.FadeOut);
        Say("...")
            .DoAfter(GiveItem(RacoonCuteDialogue.t._racoonMad))
            .DoAfter(RemoveItem(RacoonCuteDialogue.t._alreadyTalked))
            .DoAfter(new DialogueAction(()=> {
                RacoonCuteDialogue.t.UpdateState();                       
            }));
        Say("...... *rumble*")
            .DoAfter(RacoonCuteDialogue.t.FadeIn);
    }
}
public class SickRacoonDia : Dialogue {
    public SickRacoonDia(){
        Say("i swear, i really need some of that sweet medicine... and by that I mean candy");
        Say("Don't you want my little suprise that I have for you?")
            .DoAfter(new TriggerDialogueAction<NewChoiceDialogue>())
            .DoAfter(GiveItem(RacoonCuteDialogue.t._alreadyTalked));
    }
}