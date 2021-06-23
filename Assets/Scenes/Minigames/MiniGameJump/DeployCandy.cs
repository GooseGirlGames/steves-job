using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public Portal Portal;

    public GameObject candyPrefab;
    public GameObject candyPrefab2;
    private GameObject tmp;
    private GameObject tmp2;

    private GameObject bar;
    private Vector3 moveBar;

    public int respawnTime = 1;
    public int winSeconds;
    private Vector2 screenBounds;
    public Transform player;
    private bool hit = false;
    
    
    [SerializeField] private HealthBar healthbar;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(timedSpawn());
        bar = GameObject.Find("Healthbar");
        health = 1f;
        healthbar.SetSize(health);
        healthbar.SetColour(Color.green);
        winSeconds = respawnTime*15;
        StartCoroutine(winTime());
    }
    private void spawnCandylow(){
        tmp = Instantiate(candyPrefab) as GameObject;
        tmp.transform.position = new Vector2(player.position.x + 40, -screenBounds.y-2);
           
    }
    private void spawnCandyhigh(){
        tmp2 = Instantiate(candyPrefab2) as GameObject;
        tmp2.transform.position = new Vector2(player.position.x + 40, screenBounds.y-9);
    }

    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnCandylow();
            yield return new WaitForSeconds(respawnTime);
            spawnCandyhigh();
        }
        
    }
    IEnumerator healthbarFlash(){
        while(health > 0){
            healthbar.SetColour(Color.red);
            yield return new WaitForSeconds(.1f);
            healthbar.SetColour(Color.white);
            yield return new WaitForSeconds(.1f);
        }
        
    }
    IEnumerator winTime(){
        while(winSeconds != 0){
            yield return new WaitForSeconds(1);
            winSeconds -= 1;
        }
        GameWon();
    }
    public void GameLost(){
        Debug.Log("Lost Jump MiniGame");
        Portal.TriggerTeleport();
        //DialogueManager.Instance.SetInstantTrue();
    }
    public void GameWon(){
        Debug.Log("Won Jump MiniGame");
        Portal.TriggerTeleport();
    }
    public void FixedUpdate()
    {
        if(tmp != null){
            if(tmp.GetComponent<Sweets>().trigger){
                if(tmp.GetComponent<Sweets>().notYetTriggered){
                    health -= .1f; 
                }
                tmp.GetComponent<Sweets>().notYetTriggered = false;
            }
        }
        if(tmp2 != null){
            if(tmp2.GetComponent<Sweets>().trigger){
                if(tmp2.GetComponent<Sweets>().notYetTriggered){
                    health -= .1f; 
                }
                tmp2.GetComponent<Sweets>().notYetTriggered = false;
            }
        }
    }

    public void Update(){
        healthbar.SetSize(health);
        if(health <= .5f){
            healthbar.SetColour(Color.yellow);
        } 
        if(health <= .3f){
            healthbar.SetColour(Color.red);
        } 
        if(health <= .2f){
            //Debug.Log("OH NO ALMOST DEAD");
            StartCoroutine(healthbarFlash());
        } 
        if(health <= 0.0f){
            GameLost();
        } 
        
        moveBar = new Vector3(player.position.x, player.position.y +5, 1);
        bar.transform.position = moveBar;

    }


}
