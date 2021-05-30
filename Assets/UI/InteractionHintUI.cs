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

    private void Awake() {
        canvas.enabled = false;
    }
    public void Hint(Transform postion, string keyText) {
        cam = GameObject.FindObjectOfType<Camera>();
        canvas.enabled = true;
        key.text = keyText;
        target = postion;
        PositionHint();
    }

    private void PositionHint() {
        Vector3 positionOnScreen = cam.WorldToScreenPoint(target.position);
        float scale = canvas.scaleFactor;  // i think this sucks, but it works
        float hintHeight = scale * hint.GetComponentInChildren<RectTransform>().rect.size.y;
        hint.position = positionOnScreen + new Vector3(0, hintHeight/2, 0);
    }

    private void OnGUI() {
        if (IsHinting())
            PositionHint();
    }

    public void ClearHint() {
        canvas.enabled = false;
    }

    public bool IsHinting() {
        return canvas.enabled;
    }

}
