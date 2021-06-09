using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public GameObject candyPrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(timedSpawn());

    }
    private void spawnCandy(){
        GameObject tmp = Instantiate(candyPrefab) as GameObject;
        tmp.tag = "floor";
        tmp.transform.position = new Vector2(player.position.x + 40, -screenBounds.y);
    }

    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnCandy();
        }
        
    }
}
