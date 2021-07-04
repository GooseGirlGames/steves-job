using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public InteractionHintUI hintUI;
    public EventSystem EventSystem;

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
