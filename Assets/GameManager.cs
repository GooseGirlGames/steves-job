using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public InteractionHintUI hintUI;

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }
}
