using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoakedPeasantsVisible : MonoBehaviour {
    [SerializeField] private Item soaked;
    [SerializeField] new private Renderer renderer;

    private void Awake() {
        UpdateVisibility();
    }

    public void UpdateVisibility() {
        bool visible = Inventory.Instance.HasItem(soaked);
        renderer.enabled = visible;
        this.gameObject.SetActive(visible);
    }
}
