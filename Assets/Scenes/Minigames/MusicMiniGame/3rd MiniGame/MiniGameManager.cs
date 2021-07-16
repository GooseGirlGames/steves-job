
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
    private float hp = 115.0f;
    private static float MAX_HP = 120.0f;
    private static float HP_PER_SEC = -6.0f;
    private static float HP_PER_MISS =  -15.0f;
    private static float HP_PER_WRONG_PRESS = -10.0f;
    private static float HP_PER_HIT = 10.0f;
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
        Debug.Log(theMusic.clip.length);
    }
    
    void Update(){
        if(!startPlaying){
            if(Input.anyKeyDown){
                startPlaying = true;
                ddr.started = true;
                theMusic.Play();
                StartCoroutine(musicFinishing());
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
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            Vector3 steve_pos = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + 0.5f, 1);
            player.gameObject.transform.position = steve_pos;
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            player.GetComponent<stevecontroller>().crouch = true;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
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
                DialogueManager.Instance.SetInstantTrue();
                winPortal.TriggerTeleport();
            }
            if(score<= 4999){
                Inventory.Instance.AddItem(_notPowered);
                Inventory.Instance.RemoveItem(_can_use_ddr);
                DialogueManager.Instance.SetInstantTrue();
                DialogueManager.Instance.SetInstantTrue();
                winPortal.TriggerTeleport();
            }
            
        }    
    }
    private void FixedUpdate() {
        if (!startPlaying || pause) return;
        hp += HP_PER_SEC * Time.fixedDeltaTime;
        UpdateScore();
    }
    private void UpdateScore() {
        if (hp > MAX_HP) hp = MAX_HP;
        scoreText.text = "HP:" + Mathf.Clamp((int)hp, 0, 100);
    }
    public void NoteHit(){
        score += arrowScore;
        hp += HP_PER_HIT;
        UpdateScore();
    }
    public void NoteMissed() {
        hp += HP_PER_MISS;
        UpdateScore();
    }
    public void WrongPress() {
        hp += HP_PER_WRONG_PRESS;
        UpdateScore();
    }
}