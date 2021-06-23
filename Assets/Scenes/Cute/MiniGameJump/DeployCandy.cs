using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public GameObject candyPrefab;
    public GameObject candyPrefab2;

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
        health = 1f;
        healthbar.SetSize(health);

    }
    private void spawnCandylow(){
        GameObject tmp = Instantiate(candyPrefab) as GameObject;
        tmp.transform.position = new Vector2(player.position.x + 40, -screenBounds.y-4);    
    }
    private void spawnCandyhigh(){
        GameObject tmp2 = Instantiate(candyPrefab2) as GameObject;
        tmp2.transform.position = new Vector2(player.position.x + 40, screenBounds.y-7);
    }

    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnCandylow();
            yield return new WaitForSeconds(respawnTime);
            spawnCandyhigh();
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
        //Debug.Log(collisionCount);
    }


}
