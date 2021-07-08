using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Dialogue1(First):
    - The Cracked Remains of The switch,
    - Get brocken dirty switch
Dialogue2(No Power and Empty):
    - Trigger No Steve E Finished Item and broken Switch or dirty Switch or brokendirty switch or switch in inventory
    - the empty gap without switch
    - can accept switch
Dialogue3(No Power):
    - No Power here but switch(nothing happening if used)
Dialogue(Empty):
    - Dangerous, sparks are flying ect ect
    - Switch can be placed and afterwards activadet
Dialogue(Final):
    - Switch can be pressed
 */

public class SwitchDialogue : DialogueTrigger{

    public Item switch_final;
    public Item switch_dirty;
    public Item switch_broken;
    public Item switch_dirty_broken;
    public Item _powered;
    public Item _not_pickedup_with_switch;
    public static SwitchDialogue t;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(_not_pickedup_with_switch)){
            return new FirstDialogue();
        }
        else if (Inventory.Instance.HasItem(switch_broken)||Inventory.Instance.HasItem(switch_dirty)||Inventory.Instance.HasItem(switch_dirty_broken)||Inventory.Instance.HasItem(switch_final)){
            if (Inventory.Instance.HasItem(_powered)){
                return new EmptyDialogue();
            }
            else{
                return new EmptyAndPowerlessDialogue();
            }
        }
        else if(!Inventory.Instance.HasItem(_powered)){
            return new PowerlessDialogue();
        }
        return new FinalDialogue();
    }

    public class FirstDialogue : Dialogue {
        public FirstDialogue(){
            Say("Everything what is left of of this stupid switch are some broken remains");
            Say("with strange goo all over them")
                .DoAfter(GiveItem(t.switch_dirty_broken));
            Say("It is needet to repair it, and clean the thing from the sticky mess");
            Say("The Power is also down, I think Steve E Wonder has a spare generator")
                .DoAfter(RemoveItem(t._not_pickedup_with_switch));
        }
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
