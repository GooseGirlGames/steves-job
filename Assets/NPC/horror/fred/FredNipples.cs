using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredNipples : MonoBehaviour{
    [SerializeField] private Animator nipple_animator;
    [SerializeField] private List<Item> visible_shirts;
    [SerializeField] private Item _fred_has_maiddress;
    
    
    void Update() {
        bool playerHasShirt = false;
        foreach (var shirt in visible_shirts) {
            if (Inventory.Instance.HasItem(shirt)) {
                playerHasShirt = true;
                break;
            }
        }
        State nippleState = State.Shirt;
        if (playerHasShirt) {
            nippleState = State.Nipples;
        } else if (Inventory.Instance.HasItem(_fred_has_maiddress)) {
            nippleState = State.Dress;
        }
        int currentState = nipple_animator.GetInteger("NippleState");
        int targetState = (int) nippleState;

        if (currentState != targetState) {
            nipple_animator.SetInteger("NippleState", targetState);
        }
    }

    public enum State {
        Nipples = 0,
        Shirt = 1,
        Dress = 2,
    }
}
