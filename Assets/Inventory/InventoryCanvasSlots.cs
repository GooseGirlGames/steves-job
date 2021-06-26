using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryCanvasSlots : MonoBehaviour
{
    public List<InventorySlot> slots;
    public Canvas canvas;
    public bool visible = false;

    public GameObject dialogueOptionBoxes;
    public GameObject itemLoreBox;

    public static InventoryCanvasSlots Instance = null;
    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
        Clear();
        canvas.enabled = false;
    }

    public void ShowItemLoreBox(Item item) {
        dialogueOptionBoxes.SetActive(false);
        // TODO show actual lore
        itemLoreBox.SetActive(true);
    }

    public void HideItemLoreBox() {
        dialogueOptionBoxes.SetActive(true);
        itemLoreBox.SetActive(false);
    }

     public void CheckForSelectedItem() {
        foreach (InventorySlot slot in slots) {
            if(slot.button.Selected) {
                ShowItemLoreBox(slot.item);
                return;
            }
        }
        HideItemLoreBox();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && !DialogueManager.Instance.IsDialogueActive()) {
            if (visible) Hide();
            else Show();
        }

        if (visible) {
            CheckForSelectedItem();
        }
    }

    public void Show() {
            visible = true;
            canvas.enabled = true;
    }

    public void Hide() {
            visible = false;
            canvas.enabled = false;
    }

    private void Clear() {
        foreach (InventorySlot slot in slots) {
            slot.image.enabled = false;
            slot.image.sprite = null;
            slot.button.gameObject.SetActive(false);
        }
    }

    public void DisplayItems(List<Item> items) {
        Clear();
        List<Item> visibleItems = new List<Item>();
        foreach (Item item in items) {
            if (item.visible) {
                visibleItems.Add(item);
            }
        }
        int n = Mathf.Min(slots.Count, visibleItems.Count);
        for (int i = 0; i < n; ++i) {
            slots[i].image.enabled = true;
            slots[i].image.sprite = visibleItems[i].icon;
            slots[i].item = visibleItems[i];
            slots[i].button.gameObject.SetActive(true);
        }
    }
}
