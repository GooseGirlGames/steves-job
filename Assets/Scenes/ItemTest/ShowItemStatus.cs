using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemStatus : MonoBehaviour
{
    public Item item;
    void Update() {
        if (item) {
            if (Inventory.Instance.HasItem(item)) {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            } else {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
