using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitKey : MonoBehaviour
{
    public bool pressable;
    public KeyCode keyToPress;
    void Start(){
        
    }

    void Update(){
        if(Input.GetKeyDown(keyToPress)){
            if(pressable){
                gameObject.SetActive(false);
                MiniGameManager.instance.NoteHit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "button"){
            pressable = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "button"){
            pressable = false;
            MiniGameManager.instance.NoteMissed();
        }
    }
}
