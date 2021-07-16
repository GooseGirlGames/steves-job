using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public InteractionHintUI hintUI;
    public EventSystem EventSystem;
    public ItemManager itemManager;
    public readonly List<string> MINIGAME_SCENE_NAMES = new List<string>{
        "BloodFalling",
        "JumpMiniGame",
        "MusicMiniGame"
    };

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
        itemManager = GetComponent<ItemManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsInMinigame() {
        string activeScene = SceneManager.GetActiveScene().name;
        return MINIGAME_SCENE_NAMES.Contains(activeScene);
    }
}
