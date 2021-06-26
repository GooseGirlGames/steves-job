using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour {
    public Image image;
    public CoolButton button;
    public Item item;
    public void OptionChosen() {
        DialogueManager.Instance.DisplayNextSentence(item: item);
    }
/* 
    public void Hovering() {
        InventoryCanvasSlots.Instance.ShowItemLoreBox(item);
    }
    public void StoppedHovering() {
        InventoryCanvasSlots.Instance.HideItemLoreBox();
    } */
}
