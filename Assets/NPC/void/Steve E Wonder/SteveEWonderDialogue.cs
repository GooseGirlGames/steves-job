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


public class SteveEWonderDialogue : DialogueTrigger{
    // Start is called before the first frame update
    public Item _steve_horror_finished
    public Item _steve_cute_finished
    public Item _minigamewon
    public Item _;
    public static t;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(switch_broken)||Inventory.Instance.HasItem(switch_dirty)||Inventory.Instance.HasItem(switch_dirty_broken)||Inventory.Instance.HasItem(switch_final)){
            if (Inventory.Instance.HasItem(_powered)){
                return new EmptyDialogue();
            }
            else{
                return new EmptyAndPowerlessDialogue();
            }
        }
        else if(!_powered){
            return new PowerlessDialogue();
        }
        return new FinalDialogue();
    }

    public class EmptyAndPowerlessDialogue : Dialogue{
        public EmptyAndPowerlessDialogue(){
            Say("");
        }
    }
    public class EmptyDialogue : Dialogue{
        public EmptyDialogue(){
            Say("");
        }
    }
    public class PowerlessDialogue : Dialogue{
        public PowerlessDialogue(){
            Say("");
        }
    }
    
    public class FinalDialogue : Dialogue{
        public FinalDialogue(){
            Say("");
        }
    }
}