using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public Portal Portal;

    public GameObject[] candyPrefabs;
    private GameObject tmp;
    private GameObject spawn;
    private GameObject spawn2;

    private GameObject bar;
    private Vector3 moveBar;

    public int respawnTime = 1;
    public int winSeconds;
    private Vector2 screenBounds;
    public Transform player;
    private bool hit = false;
    
    
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
        Vector2 candyPrefab2_pos = new Vector2(player.position.x + 6, screenBounds.y - 1.3f);
        Vector2 candyPrefab_pos = new Vector2(player.position.x + 6, screenBounds.y - 3.0f);
        int random = Random.Range(0,candyPrefabs.Length);
        tmp = candyPrefabs[random];
        if(random == 0){
           spawn = Instantiate(tmp, candyPrefab_pos, Quaternion.identity) as GameObject;
        }
        if(random == 1){
           spawn2 = Instantiate(tmp, candyPrefab2_pos, Quaternion.identity) as GameObject;
        }       
    }

    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0.7f,4.0f));
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
    public void FixedUpdate()
    {
        if(spawn != null){
            if(spawn.GetComponent<Sweets>().trigger){
                if(spawn.GetComponent<Sweets>().notYetTriggered){
                    health -= .1f; 
                }
                spawn.GetComponent<Sweets>().notYetTriggered = false;
            }
        }
        if(spawn2 != null){
            if(spawn2.GetComponent<Sweets>().trigger){
                if(spawn2.GetComponent<Sweets>().notYetTriggered){
                    health -= .1f; 
                }
                spawn2.GetComponent<Sweets>().notYetTriggered = false;
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
        
        moveBar = new Vector3(player.position.x, player.position.y + 0.8f, 1);
        bar.transform.position = moveBar;

    }


}
