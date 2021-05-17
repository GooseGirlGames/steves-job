using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {

    public enum OriginWorld {
         Normal,
         Cute,
         Horror,
         Void,
    }
    new public string name = "";
    public string description = "";
    public bool visible = false;
    public Sprite icon = null;
    public OriginWorld originWorld;
    public bool unique = false;
    public List<Item> conflictingItems;
    
}
