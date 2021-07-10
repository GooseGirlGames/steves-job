using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGame : MonoBehaviour{
    public Sprite defaultImage;
    public Sprite hit;
    public Sprite pressed;
    public Sprite failed;

    public KeyCode keyToPress;

  /*   void Start(){
        sp = GetComponent<Image>();
    } */

    void Update(){
        if(Input.GetKeyDown(keyToPress)){
            gameObject.GetComponent<Image>().sprite = pressed;
        }
        if(Input.GetKeyUp(keyToPress)){
            gameObject.GetComponent<Image>().sprite = defaultImage;
        }
    }
}