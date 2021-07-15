using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {

    new public string name = "";
    public string description = "";
    public bool visible = false;
    public Sprite icon = null;
    public World originWorld;
    public bool unique = false;
    public List<Item> conflictingItems;
    public bool isRestoreItem = false;
    public Item restoreItemCounterpart = null;
    
}
