using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGame : MonoBehaviour{
    public Sprite defaultImage_l;
    public Sprite defaultImage_r;
    public Sprite defaultImage_u;
    public Sprite defaultImage_d;
    public Sprite pressed_l;
    public Sprite pressed_r;
    public Sprite pressed_u;
    public Sprite pressed_d;


    void Update(){
        if(Input.GetAxis("Vertical") < 0){
            GameObject button = GameObject.Find("arrow_down");
            if(Input.GetButtonDown("Vertical")){
                button.GetComponent<SpriteRenderer>().sprite = pressed_d;
            }
            if(Input.GetButtonUp("Vertical")){
                button.GetComponent<SpriteRenderer>().sprite = defaultImage_d;
            }
        }

        if(Input.GetAxis("Vertical") > 0) {
            GameObject button = GameObject.Find("arrow_up");
            if(Input.GetButtonDown("Vertical")){
                button.GetComponent<SpriteRenderer>().sprite = pressed_u;
            }
            if(Input.GetButtonUp("Vertical")){
                button.GetComponent<SpriteRenderer>().sprite = defaultImage_u;
            }
        }
        if(Input.GetAxis("Horizontal") < 0){
            GameObject button = GameObject.Find("arrow_left");
            if(Input.GetButtonDown("Horizontal")){
                button.GetComponent<SpriteRenderer>().sprite = pressed_l;
            }
            if(Input.GetButtonUp("Horizontal")){
                button.GetComponent<SpriteRenderer>().sprite = defaultImage_l;
            }
        }
        if(Input.GetAxis("Horizontal") > 0){
            GameObject button = GameObject.Find("arrow_right");
            if(Input.GetButtonDown("Horizontal")){
                button.GetComponent<SpriteRenderer>().sprite = pressed_r;
            }
            if(Input.GetButtonUp("Horizontal")){
                button.GetComponent<SpriteRenderer>().sprite = defaultImage_r;
            }
        }
    }
}