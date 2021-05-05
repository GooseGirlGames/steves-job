using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryCanvasSlots : MonoBehaviour
{
    public List<Image> slots;
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
        foreach (Image image in slots) {
            image.enabled = false;
            image.sprite = null;
        }
    }

    public void DisplayItems(List<Item> items) {
        Clear();
        int n = Mathf.Min(slots.Count, items.Count);
        for (int i = 0; i < n; ++i) {
            slots[i].enabled = true;
            slots[i].sprite = items[i].icon;
        }
    }
}
