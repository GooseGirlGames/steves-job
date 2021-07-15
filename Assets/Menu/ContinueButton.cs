using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ContinueButton : MonoBehaviour {
    private TextMeshProUGUI text;
    public Portal enterPortal;
    private SaveGame s;
    private void Awake() {
        Debug.Log("Good morning, I am a continue butyton");
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = SaveLoadSystem.SaveExists() ? "Continue" : "Go to Work";
        if (SaveLoadSystem.SaveExists()) {
            s = SaveLoadSystem.LoadFromFile();
            enterPortal.targetSceneName = s.scene;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("ONSCENELOADED");
        var playerPos = new Vector3(s.pos_x, s.pos_y, s.pos_z);
        Inventory.Instance.items = GameManager.Instance.itemManager.FromIdList(s.inventory);
        GameObject.FindObjectOfType<stevecontroller>().transform.position
                = playerPos + new Vector3(0, 0.1f, 0);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
