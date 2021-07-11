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
    public static SteveEWonderDialogue t;

    public override void UpdateStaticT() {
        t = this;
    }

    public override Dialogue NewFullRestoredDia() {
        if (!Inventory.Instance.HasItem(_said_thanks)) {
            return new Thanks();  // Dialogue3
        } else {
            if (!Inventory.Instance.HasItem(_powered)) {
                return new OfferMinigame();  // Dialogue4
            } else {
                return new MinigameComplete();  // Dialogue5
            }
        }
    }
    public override Dialogue NewHalfRestoredDia() => new HalfRestoredDia();  // Dialogue2
    public override Dialogue NewGoneDia() => new GoneDia();  // Dialogue1
    public class MinigameComplete : Dialogue {
        public MinigameComplete() {  // Dialogue5
            Say("MinigameComplete/ Dialogue5");
        }
    }
    public class OfferMinigame : Dialogue {
        public OfferMinigame() {  // Dialogue4
            Say("OfferMinigame / Dialogue4");
        }
    }
    public class Thanks : Dialogue {
        public Thanks() {  // Dialogue3
            Say("Thanks / Dialogue3");
        }
    }
    public class HalfRestoredDia : Dialogue {
        public HalfRestoredDia() {  // Dialogue2
            Say("...?");
        }
    }
    public class GoneDia : Dialogue {
        public GoneDia() {  // Dialogue1
            Say("...");
        }
    }
}