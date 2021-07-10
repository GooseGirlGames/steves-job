using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour{

    public AudioSource theMusic;
    public bool startPlaying;
    public KindaDDR ddr;
    public static MiniGameManager instance;

    void Start(){
        instance = this;
    }
    
    void Update(){
        if(!startPlaying){
            if(Input.anyKeyDown){
                startPlaying = true;
                ddr.started = true;

                theMusic.Play();
            }
        }
    }
    public void NoteHit(){
        Debug.Log("hit");
    }
    public void NoteMissed(){
        Debug.Log("missed");

    }
}
