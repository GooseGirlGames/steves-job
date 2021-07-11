using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitKey : MonoBehaviour
{
    public bool pressable;
    public bool hit = false;
    public bool missed = false;
    public KeyCode keyToPress;
    

    void Update(){
        if(Input.GetKeyDown(keyToPress)){
            if(pressable){
                MiniGameManager.instance.NoteHit();
                gameObject.SetActive(false);
            }
            
        }
/*         if(pressable && !hit){
            if(!missed){
                missed = true; 
                MiniGameManager.instance.NoteMissed();
            }   
        }       
     */
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
