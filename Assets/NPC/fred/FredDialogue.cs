using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredDialogue : DialogueTrigger
{
    public Item shirt;

/*     public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Trigger();
        }
    } */

    public void GiveShirt() {
        Inventory.Instance.AddItem(shirt);
    }

    [SerializeField]
    public Dialogue dialogue;
    public Dialogue shirtNotBackDia;

    public override Dialogue GetActiveDialogue() {
        if(Inventory.Instance.HasItem(shirt)){
            return shirtNotBackDia;
        }
       return dialogue; 
    }
}
