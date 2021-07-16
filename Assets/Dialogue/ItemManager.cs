using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public List<Item> allItems = new List<Item>();
    private void Awake() {
        allItems.AddRange(Resources.LoadAll<Item>("visible"));
        allItems.AddRange(Resources.LoadAll<Item>("invisible"));

        Debug.Log("GOT A TOTAL OF " + allItems.Count + " ITEMS!");

        foreach (Item a in allItems) {
            foreach (Item b in allItems) {
                if (a != b && a.id == b.id) {
                    throw new System.Exception("Items " + a.name + " and " + b.name +
                            " have same id ("+ a.id + ").  Save/Load *WILL* bork.");
                }
            }
        }
    }
    public List<int> ToIdList(List<Item> items) {
        List<int> ids = new List<int>();
        foreach (Item i in items)
            ids.Add(i.id);
        return ids;
    }
    public List<Item> FromIdList(List<int> ids) {
        List<Item> items = new List<Item>();
        foreach (int id in ids) {
            foreach (Item i in allItems) {
                if (i.id == id) {
                    items.Add(i);
                }
            }
        }
        return items;
    }
}
