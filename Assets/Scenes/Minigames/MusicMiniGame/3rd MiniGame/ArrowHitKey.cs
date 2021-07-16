using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitKey : MonoBehaviour
{
    public bool pressable;
    public bool hit = false;
    public bool missed = false;
    public KeyCode keyToPress;
    public KeyCode altKey;
    public Sprite pressed;
    private SpriteRenderer sprite;
    private bool hit_registered_with_minigame_manager = false;

    void Start(){
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(Input.GetKeyDown(keyToPress) || Input.GetKeyDown(altKey)){
            
            if(pressable){
                hit_registered_with_minigame_manager = true;
                MiniGameManager.instance.NoteHit();
                gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "button"){
            pressable = true;
            sprite.sprite = pressed;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "button"){
            pressable = false;
            sprite.color = Color.red;
            if (!hit_registered_with_minigame_manager)
                MiniGameManager.instance.NoteMissed();
        }
    }
}
