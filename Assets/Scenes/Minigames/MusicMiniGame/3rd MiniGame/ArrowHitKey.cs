using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitKey : MonoBehaviour
{
    public bool pressable;
    public bool hit = false;
    public bool missed = false;
    public Sprite hitSprite;
    private bool button_direction;

    void Update(){
        if(button_direction){
            gameObject.GetComponent<SpriteRenderer>().sprite = hitSprite;
            if(pressable){
                
                MiniGameManager.instance.NoteHit();
                gameObject.SetActive(false);
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "button"){
            pressable = true;
            if(other.gameObject.name == "arrow_left"){
                //if(Input.GetAxis("Horizontal") < 0){
                    button_direction = Input.GetAxis("Horizontal") < 0/* Input.GetButtonDown("Horizontal") */;
                //}
                
            }
            if(other.gameObject.name == "arrow_right"){
                //if(Input.GetAxis("Horizontal") > 0){
                    button_direction = Input.GetAxis("Horizontal") > 0/* Input.GetButtonDown("Horizontal") */;
                //}
            }
            if(other.gameObject.name == "arrow_up"){
                //if(Input.GetAxis("Vertical") > 0){
                    button_direction = Input.GetAxis("Vertical") > 0/* Input.GetButtonDown("Vertical") */;
                //}   
            }
            if(other.gameObject.name == "arrow_up"){
                //if(Input.GetAxis("Vertical") < 0){
                    button_direction = Input.GetAxis("Vertical") < 0/* Input.GetButtonDown("Vertical") */;
                //}   
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "button"){
            pressable = false;
        }
    }
}
