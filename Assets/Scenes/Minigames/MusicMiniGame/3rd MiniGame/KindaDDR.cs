using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindaDDR : MonoBehaviour{
    
public float beatTempo;
public bool started;

    void Start(){
        beatTempo = beatTempo/60f;
    }

    void Update(){
        if(!started){
           /*  if(Input.anyKeyDown){
                started = true;
            } */
        }
        else{
            transform.position -= new Vector3(0f, -beatTempo*Time.deltaTime, 0f);
        }
    }
}
