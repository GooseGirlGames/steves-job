using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance = null;

    public List<Item> items = new List<Item>();
    public InventoryCanvasSlots inventoryCanvasSlots;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("Multiple instances of inventory created.");
            return;
        }

        Instance = this;
    }

    public void AddItem(Item item) {
        items.Add(item);
        InventoryChanged();
    }

    public string RemoveItem(Item item) {
        string name = item.name;
        items.Remove(item);
        InventoryChanged();
        return name;
    }

    private void InventoryChanged() {
        inventoryCanvasSlots.DisplayItems(items);
    }

    public bool HasItem(Item item) {
        return items.Contains(item);
    }

    public bool HasItemByName(string name) {
        foreach (Item item in items) {
            if (item.name.Equals(name)) {
                return true;
            }
        }
        return false;
    }
}
