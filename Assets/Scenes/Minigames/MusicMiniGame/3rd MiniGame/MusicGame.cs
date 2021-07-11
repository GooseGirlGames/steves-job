using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGame : MonoBehaviour{
    public Sprite defaultImage;
    public Sprite pressed;
 

    public KeyCode keyToPress;

  /*   void Start(){
        sp = GetComponent<Image>();
    } */

    void Update(){
        if(Input.GetKeyDown(keyToPress)){
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
        }
        if(Input.GetKeyUp(keyToPress)){
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultImage;
        }
    }
}