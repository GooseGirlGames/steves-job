using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour{

    public AudioSource theMusic;
    public bool startPlaying;
    public KindaDDR ddr;
    public static MiniGameManager instance;
    public int score;
    public int arrowScore = 100;
    public Text scoreText;
    public GameObject instruction;
    public Portal winPortal;
    public Portal losePortal;
    private bool pause = false;

    void Start(){
        instance = this;
        scoreText.text = "Score: 0";
        StartCoroutine(musicFinishing());
        Debug.Log(theMusic.clip.length);
    }
    
    void Update(){
        if(!startPlaying){
            if(Input.anyKeyDown){
                startPlaying = true;
                ddr.started = true;
                theMusic.Play();
                instruction.SetActive(false);
            }
        }
        if(PauseMenu.paused){
            theMusic.Pause();
            pause = true;
        }
        if(!PauseMenu.paused && pause){
            theMusic.Play();
            pause = false;
        }
    }

    IEnumerator musicFinishing(){
        while(true){
            yield return new WaitForSeconds(theMusic.clip.length);
            if(score >= 5000){
                winPortal.TriggerTeleport();
            }
            if(score<= 4999){
                losePortal.TriggerTeleport();
            }
            else Debug.Log("huh?");
        }    
    }
    public void NoteHit(){
        score += arrowScore;
        scoreText.text = "Score: " + score;
    }
/*     public void NoteMissed(){
        Debug.Log("missed");

    } */
}
