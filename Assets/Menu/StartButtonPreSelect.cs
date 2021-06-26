using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonPreSelect : MonoBehaviour
{
    public Button button;
    private void Awake() {
        StartCoroutine(DialogueManager.SelectContinueButtonLater(button));
    }
}
