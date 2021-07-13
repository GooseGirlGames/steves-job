using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGame : MonoBehaviour{
    public Sprite defaultImage;
    public Sprite pressed;
 

    public KeyCode keyToPress;
    public KeyCode altKey;


    void Update(){
        if(Input.GetKeyDown(keyToPress) || Input.GetKeyDown(altKey)){
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
        }
        if(Input.GetKeyUp(keyToPress) || Input.GetKeyUp(altKey)){
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultImage;
        }
    }
}