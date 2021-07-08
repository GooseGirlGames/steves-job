using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseVisible : MonoBehaviour {
    public Item joined;

    void Update() {
        bool visible = !Inventory.Instance.HasItem(joined);
        GetComponent<Renderer>().enabled = visible;
    }
}
