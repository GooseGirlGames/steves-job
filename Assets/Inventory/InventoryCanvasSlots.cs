using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryCanvasSlots : MonoBehaviour
{
    public List<InventorySlot> slots;
    public Canvas canvas;
    private bool visible = false;

    private void Awake() {
        Clear();
        canvas.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            visible = !visible;
            canvas.enabled = visible;
        }
    }

    private void Clear() {
        foreach (InventorySlot slot in slots) {
            slot.image.enabled = false;
            slot.image.sprite = null;
            slot.name.enabled = false;
            slot.name.text = "";
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
            slots[i].name.enabled = true;
            slots[i].name.text = items[i].name;
        }
    }
}
