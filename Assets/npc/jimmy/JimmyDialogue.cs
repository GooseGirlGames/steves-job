using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : DialogueTrigger
{
    public Portal portalToMiniGame;
    public Item bucket;
    public Item empty;


    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    [SerializeField]
    public Dialogue dialogue;
    public Dialogue winlogue;
    public Dialogue loserdia;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(bucket)) {
            return winlogue;
            
        }
        else if (Inventory.Instance.HasItem(empty)){
            return loserdia;
        }
       return dialogue; 
    }
}
