using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonHit : MonoBehaviour
{
    public bool hit = false;
    public Sprite rancoonThrow;
    public Sprite runningRacoon;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "spawn"){
            hit = true;
            GetComponent<SpriteRenderer>().sprite = rancoonThrow;
            //Debug.Log("hit");
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "spawn"){
            GetComponent<SpriteRenderer>().sprite = runningRacoon;
        }
    }

}
