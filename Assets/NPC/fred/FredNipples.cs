using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredNipples : MonoBehaviour{
    [SerializeField] private Animator nipple_animator;
    [SerializeField] private List<Item> shirts;
    
    void Update() {
        bool playerHasShirt = false;
        foreach (var shirt in shirts) {
            if (Inventory.Instance.HasItem(shirt)) {
                playerHasShirt = true;
                break;
            }
        }
        nipple_animator.SetBool("shirt", !playerHasShirt);
    }
}
