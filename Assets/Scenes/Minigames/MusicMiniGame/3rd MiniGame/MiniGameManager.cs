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
    public GameObject player;
    public Item _powered;
    public Item _notPowered;
    public Item _can_use_ddr;

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
        if(Input.GetAxis("Vertical") > 0){
            if(Input.GetButtonDown("Vertical")){
                Vector3 steve_pos = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + 0.5f, 1);
                player.gameObject.transform.position = steve_pos;
            }
        }
        if(Input.GetAxis("Vertical") < 0){
            if(Input.GetButtonDown("Vertical")){
                if(player.GetComponent<stevecontroller>().is_grounded){
                    player.GetComponent<stevecontroller>().crouch = true;
                }    
            }
        }
        if(Input.GetButtonUp("Vertical")){
            player.GetComponent<stevecontroller>().crouch = false;

        }
    }

    IEnumerator musicFinishing(){
        while(true){
            yield return new WaitForSeconds(theMusic.clip.length);
            if(score >= 5000){
                Inventory.Instance.AddItem(_powered);
                Inventory.Instance.RemoveItem(_notPowered);
                Inventory.Instance.RemoveItem(_can_use_ddr);
            }
            if(score<= 4999){
                Inventory.Instance.AddItem(_notPowered);
            }
            winPortal.TriggerTeleport();
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
