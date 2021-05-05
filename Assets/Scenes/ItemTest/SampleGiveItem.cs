using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGiveItem : MonoBehaviour
{
    public Item item;
    public void Trigger() {
        Inventory.Instance.AddItem(item);
    }
}
