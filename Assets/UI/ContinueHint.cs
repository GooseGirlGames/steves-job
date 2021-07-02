using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ContinueHint : MonoBehaviour {
    private CanvasRenderer[] renderers;  // renderers for text and background
    [SerializeField] private TextMeshProUGUI key;

    public abstract bool IsShown();
    private void Awake() {
        renderers = GetComponentsInChildren<CanvasRenderer>();
        key.text = DialogueManager.DIALOGUE_KEY_STR;
    }
    private void LateUpdate() {
        bool enabled = IsShown();
        foreach (CanvasRenderer r in renderers) {
            r.SetAlpha(enabled ? 1 : 0);
        }
    }
}
