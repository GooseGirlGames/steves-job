using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManagement : MonoBehaviour {
    public static SceneTransitionManagement Instance = null;
    public List<Image> goosepics;
    public List<Sprite> gooseSprites;
    public List<Item> geese;
    private void UpdateGoose() {

        for (int gooseIdx = 0; gooseIdx < geese.Count; ++gooseIdx) {
            Debug.Log(gooseIdx);
            if (Inventory.Instance.HasItem(geese[gooseIdx])) {
                Debug.Log("Found goose");
                foreach (var pic in goosepics) {   
                    pic.sprite = gooseSprites[gooseIdx];
                    pic.color = Color.white;
                }
                return;
            }
        }
        Debug.Log("nogoose");
        foreach (var pic in goosepics) {   
            pic.color = Color.clear;
        }
    }
    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    public Animator GetAnimator(World w) {
        switch (w) {
            case World.Cute: return cuteAnim;
            case World.Horror: return horrorAnim;
            case World.Void: return voidAnim;
        }
        return null;
    }
    public Animator GetSteveAnimator(World w) {
        UpdateGoose();
        switch (w) {
            case World.Cute: return steveCuteAnim;
            case World.Horror: return steveHorrorAnim;
            case World.Void: return steveVoidAnim;
        }
        return null;
    }

    public Animator black;
    public Animator horrorAnim;
    public Animator cuteAnim;
    public Animator voidAnim;
    public Animator steveHorrorAnim;
    public Animator steveCuteAnim;
    public Animator steveVoidAnim;
}
