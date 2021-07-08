using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public Portal Portal;

    public GameObject racoon;
    private Vector3 racoon_pos;
    public RacoonHit racoonHit;

    int rand_spawn;
    public GameObject[] candyPrefabs;
    public GameObject spawns;
    public Transform[] spawn_arr;
    private Vector3 spawn_pos;
    private Vector2 candyPrefab_pos;
    private GameObject tmp;
    private GameObject spawn;

    private GameObject bar;
    private Vector3 moveBar;

    public int respawnTime = 1;
    public int winSeconds;
    private Vector2 screenBounds;
    private Vector2 bounds;
    public Transform player;
    public GameObject stop;
    
    
    [SerializeField] private HealthBar healthbar;
    private float health;

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

    private void spawnCandy(){
        racoonHit = racoon.GetComponent<RacoonHit>();
               
        int rand_spawn = Random.Range(0,spawn_arr.Length);
        int random = Random.Range(0,candyPrefabs.Length);
        Transform rand_transform = spawn_arr[rand_spawn];
        candyPrefab_pos = new Vector2(rand_transform.position.x, rand_transform.position.y); 
        tmp = candyPrefabs[random];
        spawn = Instantiate(tmp, candyPrefab_pos, Quaternion.identity) as GameObject;   
    }


    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(Random.Range(2.0f,4.0f));
            spawnCandy();
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
    }
    public void GameWon(){
        Debug.Log("Won Jump MiniGame");
        Portal.TriggerTeleport();
    }
    public void FixedUpdate(){
        if(spawn != null){
            if(spawn.GetComponent<Sweets>().notYetTriggered){
                if(spawn.GetComponent<Sweets>().trigger){
                    health -= .1f; 
                    spawn.GetComponent<Sweets>().notYetTriggered = false;
                }
                
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
            StartCoroutine(healthbarFlash());
        } 
        if(health <= 0.0f){
            GameLost();
        } 

        racoon_pos = new Vector3(player.position.x + 4.0f, racoon.transform.position.y);
        racoon.transform.position = racoon_pos;
        
        
        moveBar = new Vector3(player.position.x, player.position.y + 0.8f, 1);
        bar.transform.position = moveBar;
        Vector2 bound_pos = new Vector2(stop.transform.position.x,player.position.y);
        if(player.position.x > bound_pos.x + 1.0f){
            bound_pos.x = player.position.x - 1.0f; 
        }
        stop.transform.position = bound_pos;

        spawn_pos = new Vector3(racoon_pos.x + 1.0f, spawns.transform.position.y, 1);
        spawns.transform.position = spawn_pos;

    }


}
