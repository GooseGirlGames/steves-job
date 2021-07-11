using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManagement : MonoBehaviour {
    public static SceneTransitionManagement Instance = null;
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