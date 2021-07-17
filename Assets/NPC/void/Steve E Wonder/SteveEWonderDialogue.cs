using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Personality:
    - 
Dialogue1(Steve Is non Existend):+
    - Just dont say anything, has no skin
Dialogue2(Steve Is half Restored):
    - flackert sagt eine Zeile text, ist verwirrt
Dialogue3(Steve IS Restored and first interaction before minigame):
    - Bedankt sich, erklärt wie er sich gefühlt hat, 
    - bietet an strom durch musik zu machen braucht aber hilfe um in stimmung zu kommen 
Dialogue4(Steve Is Restored and before minigame):
    - Bietet minispiel an
    - if win your get _powerd
Dialogue5(Steve is Restored and after minigame):
    - Starts making musik
    - is exausted and give power
*/


public class SteveEWonderDialogue : VoidNPCDiaTrigger {
    public Item _powered;
    public Item _said_thanks;
    public Item _can_use_ddr;
    public Item _notPowered;

    public static SteveEWonderDialogue t;

    public override void UpdateStaticT() {
        t = this;
    }

    public override Dialogue NewFullRestoredDia() {
/*         if (!Inventory.Instance.HasItem(_said_thanks)) {
              // Dialogue3
        } */
        if(Inventory.Instance.HasItem(_said_thanks)){
            return new Thanks2();
        }
        if(Inventory.Instance.HasItem(_can_use_ddr)) {
                return new OfferMinigame();  // Dialogue4
        } 
        if(Inventory.Instance.HasItem(_powered)) {
            return new MinigameComplete();  // Dialogue5
        }
        if(Inventory.Instance.HasItem(_notPowered)){
            return new MinigameLost();
        }
        return new Thanks();
    }
    public override Dialogue NewHalfRestoredDia() => new HalfRestoredDia();  // Dialogue2
    public override Dialogue NewGoneDia() => new GoneDia();  // Dialogue1
    public class MinigameComplete : Dialogue {
        public MinigameComplete() {  // Dialogue5
            Say("♪Tell me something good♪ this is ♪so amazing♪");
            Say("this just ♪knocks me of my feet♪ you did good kiddo, well done!");
        }
    }
    public class OfferMinigame : Dialogue {
        public OfferMinigame() {  // Dialogue4
            Say("this light switch? ♪it ain't no use♪ ♪look around♪ see this machine over there?");
            Say("this will give us the power we need you should go over there and use it ♪get it♪?");
        }
    }
    public class Thanks : Dialogue {
        public Thanks() {  // Dialogue3
            Say("♪It's you♪");
            Say("♪from the bottom of my heart♪ Thank you so much for saving me");
            Say(" but ♪That's what friends are for♪ right?");
            Say("this void felt like.. ♪something out of the blue♪");
            Say("now ♪for once in my life♪ I know that ♪we can work it out♪")
                .DoAfter(new TriggerDialogueAction<Thanks2>())
                .Do(GiveItem(t._said_thanks));

        }
    }
    public class MinigameLost : Dialogue {
        public MinigameLost() {
            Say("Your vibe was off...");
            Say("you should talk to the machine again! ♪get it♪?");
        }
    }
    public class Thanks2 : Dialogue {
        public Thanks2() {  // Dialogue3 pt. 2
            Say("♪look around♪ there is no electricity. You must be the janitor! ♪do yourself a favour♪ and help me to get it back")
                .Choice(
                    new TextOption("Sure!")
                    .IfChosen(GiveItem(t._can_use_ddr))
                    .IfChosen(RemoveItem(t._said_thanks))
                    .IfChosen(new TriggerDialogueAction<OfferMinigame>()))
                .Choice(
                    new TextOption("maybe later")
                );
                
        }
    }
    public class HalfRestoredDia : Dialogue {
        public HalfRestoredDia() {  // Dialogue2
            Say("♪I'm wondering♪");
        }
    }
    public class GoneDia : Dialogue {
        public GoneDia() {  // Dialogue1
            Say("...");
        }
    }
}