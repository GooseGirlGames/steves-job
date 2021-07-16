using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGame : MonoBehaviour{
    public Sprite defaultImage;
    public Sprite pressed;
 

    public KeyCode keyToPress;
    public KeyCode altKey;
    private bool arrowInside;

    private void OnTriggerEnter2D(Collider2D other) {
        arrowInside = true;
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
    void OnTriggerExit2D(Collider2D other) {
        arrowInside = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Update(){
        if(Input.GetKeyDown(keyToPress) || Input.GetKeyDown(altKey)){
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
            if (!arrowInside) MiniGameManager.instance.WrongPress();
        }
        if(Input.GetKeyUp(keyToPress) || Input.GetKeyUp(altKey)){
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultImage;
        }
    }
}