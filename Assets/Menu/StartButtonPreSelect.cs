using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButtonPreSelect : MonoBehaviour
{
    public Button button;
    private void Start() {
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }
    private void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            StartCoroutine(UIUtility.SelectButtonLater(button));
        }
    }
}
