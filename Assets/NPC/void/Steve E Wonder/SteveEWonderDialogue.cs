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
    public Item _steve_horror_finished;
    public Item _steve_cute_finished;
    public Item _minigamewon;
    public static SteveEWonderDialogue t;

    public override Dialogue GetActiveDialogue() {
        return new NoneExist();
    }

    public class NoneExist : Dialogue{
        public NoneExist(){
            Say("...");
        }
    }
}