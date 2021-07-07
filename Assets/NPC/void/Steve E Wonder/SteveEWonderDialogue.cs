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
    public Item switch_final;
    public Item switch_dirty;
    public Item switch_broken;
    public Item switch_dirty_broken;
    public Item _powered;
    public static SwitchDialogue t;

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