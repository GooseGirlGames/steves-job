using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance = null;

    public List<Item> items = new List<Item>();
    public InventoryCanvasSlots inventoryCanvasSlots;
    public bool has_shirt = false;

    [SerializeField] private List<Item> coins = new List<Item>();

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("Multiple instances of inventory created.");
            return;
        }
        Instance = this;
    }

    public void AddItem(Item item) {
        if (item.unique && HasItem(item)){
            return;
        }
        foreach (Item conflict in item.conflictingItems){
            if (HasItem(conflict)){
                RemoveItem(conflict);
            } 
        }
        ItemNotification.Instance.NotifyItem(item, recieved: true);
        items.Add(item);
        InventoryChanged();    
    }

    public string RemoveItem(Item item) {
        string name = item.name;
        if (HasItem(item)) {
            ItemNotification.Instance.NotifyItem(item, recieved: false);
        }
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

    public int CoinBalance() {
        List<Item> coins_in_inventory = items.FindAll(
            (item) => coins.Contains(item)
        );
        return coins_in_inventory.Count;
    }
}
