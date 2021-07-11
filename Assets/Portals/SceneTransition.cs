using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    public CanvasRenderer black;
    public CanvasRenderer horror;
    private void Awake() {
        //black.SetAlpha(0.5f);
        //horror.SetAlpha(0.5f);
    }
    public void TransitionBlack() {
        //black.SetAlpha(1.0f);
        Debug.Log("Black");
    }
    public void TransitionHorror() {
        Debug.Log("Horror");
    }
    public void TransitionCute() {
        TransitionBlack();
    }
    public void TransitionVoid() {
        TransitionBlack();
    }

}
