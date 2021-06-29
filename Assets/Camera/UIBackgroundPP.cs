using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIBackgroundPP : MonoBehaviour {
    public Volume volume;
    void Update() {
        bool show =
                DialogueManager.Instance.IsDialogueActive()
                || InventoryCanvasSlots.Instance.IsShowing();
        volume.weight = show ? 1 : 0;
    }
}
