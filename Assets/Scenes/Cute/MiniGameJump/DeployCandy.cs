using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public GameObject candyPrefab;
    public GameObject candyPrefab2;
    private GameObject bar;
    private Vector3 moveBar;
    public int respawnTime = 1;
    private Vector2 screenBounds;
    public Transform player;
    int collisionCount = 0;
    
    [SerializeField] private HealthBar healthbar;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(timedSpawn());
        bar = GameObject.Find("Healthbar");
        health = .2f;
        healthbar.SetSize(health);
        healthbar.SetColour(Color.green);

        if(health <= .3f){
            StartCoroutine(healthbarFlash());
        }
        

    }
    private void spawnCandylow(){
        GameObject tmp = Instantiate(candyPrefab) as GameObject;
        tmp.transform.position = new Vector2(player.position.x + 40, -screenBounds.y-2);    
    }
    private void spawnCandyhigh(){
        GameObject tmp2 = Instantiate(candyPrefab2) as GameObject;
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

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Trigger");
        /* if (col.attached){
            collisionCount +=1;
            Debug.Log("HIT");
        } */
    }
    public void Update(){
        /* if(health <= .3f){
            Debug.Log("OH NO ALMOST DEAD");
            StartCoroutine(healthbarFlash());
        } */
        moveBar = new Vector3(player.position.x, player.position.y +5, 1);
        bar.transform.position = moveBar;
        //Debug.Log(collisionCount);
    }


}
