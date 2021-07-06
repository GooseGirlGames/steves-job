using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoakedPeasantsVisible : MonoBehaviour {
    [SerializeField] private Item soaked;
    [SerializeField] new private Renderer renderer;

    private void Awake() {
        renderer.enabled = Inventory.Instance.HasItem(soaked);
    }
}
