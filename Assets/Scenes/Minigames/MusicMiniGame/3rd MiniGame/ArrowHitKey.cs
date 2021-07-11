using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitKey : MonoBehaviour
{
    public bool pressable;
    public bool hit = false;
    public bool missed = false;
    public KeyCode keyToPress;
    void Start(){
        
    }

    void Update(){
        if(Input.GetKeyDown(keyToPress)){
            if(pressable){
                hit = true;
                gameObject.SetActive(false);
                
            }
            if(hit){
                MiniGameManager.instance.NoteHit();
            }
            
        }
        if(pressable && !hit){
            if(!missed){
                missed = true; 
                MiniGameManager.instance.NoteMissed();
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
        }
    }
}
