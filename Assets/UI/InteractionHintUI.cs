using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InteractionHintUI : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI key;
    public Transform hint;
    private Transform target;
    private Camera cam;
    private Vector3 positionOffset;

    private void Awake() {
        ClearHint();
    }
    public void Hint(Transform postion, string keyText) {
        Hint(postion, keyText, Vector3.zero);
    }
    public void Hint(Transform postion, string keyText, Vector3 offset) {
        cam = GameObject.FindObjectOfType<Camera>();
        canvas.enabled = true;
        key.text = keyText;
        target = postion;
        positionOffset = offset;
        PositionHint();
    }

    private void PositionHint() {
        Vector3 positionOnScreen = cam.WorldToScreenPoint(target.position);
        float scale = canvas.scaleFactor;  // i think this sucks, but it works
        float hintHeight = scale * hint.GetComponentInChildren<RectTransform>().rect.size.y;
        hint.position = positionOnScreen + new Vector3(0, hintHeight/2, 0) + positionOffset;
    }

    private void OnGUI() {
        if (IsHinting() && target)  // Ensure we actually have a hint location
            PositionHint();
    }

    public void ClearHint() {
        canvas.enabled = false;
    }

    public bool IsHinting() {
        return canvas.enabled;
    }

}
