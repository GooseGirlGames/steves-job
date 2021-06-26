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
    private bool loreVisible = false;
    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
        Clear();
        canvas.enabled = false;
    }
    public void SetActionBoxVisibility(bool visible) {
        // not that big of a problem anymore, huh?
        dialogueOptionBoxes.transform.localScale =
                visible ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
    }
    public void ShowItemLoreBox(Item item) {
        if (!Inventory.Instance.HasItem(item)) {
            HideItemLoreBox();
        }
        Debug.Log("Acraulyy, we're *showing* lroe for " + item.name);
        SetActionBoxVisibility(false);
        // TODO show actual lore
        itemLoreBox.SetActive(true);
        loreVisible = true;
    }

    public void HideItemLoreBox() {
        SetActionBoxVisibility(true);
        itemLoreBox.SetActive(false);
        loreVisible = false;
    }

     public void CheckForSelectedItem() {
        foreach (InventorySlot slot in slots) {
            if(slot.button.IsActive() && slot.button.Selected) {
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

        /*if (loreVisible) {
            if (Input.GetKeyDown(KeyCode.D)) {
                foreach (InventorySlot slot in slots) {
                    slot.button.Selected = false;
                }
                HideItemLoreBox();
            }
        }*/
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
