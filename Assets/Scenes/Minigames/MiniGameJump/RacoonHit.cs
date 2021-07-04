using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonHit : MonoBehaviour
{
    public bool hit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "spawn"){
            hit = true;
            //Debug.Log("hit");
        }
    }

}
