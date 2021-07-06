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

    public Item;
    public Item;
    public Item;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.CoinBalance() == 0) {
            return new NoCoins();
        }

        if (!Inventory.Instance.HasItem(item_for_sale)) {
            return new BuyItem(this);
        } else {
            return new ItemBought();
        }
    }
}
