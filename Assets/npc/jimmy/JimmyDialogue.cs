using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : DialogueTrigger
{
    public Portal portalToMiniGame;
    public Item bucket;
    public Item empty;

/*     public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided!");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("with player");
            Trigger();
        }
    }
 */
    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    [SerializeField]
    public Dialogue dialogue;
    public Dialogue winlogue;
    public Dialogue loserdia;
    public Dialogue tmp;
    public bool interaction = false;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(bucket)) {
            return winlogue;
        }
        else if (Inventory.Instance.HasItem(empty)){
            DialogueManager.Instance.setInstantTrue();
            Debug.Log("true");
            return loserdia;
        }
        else if (!interaction){
            return dialogue;
        }
           return tmp; 

    }
}
