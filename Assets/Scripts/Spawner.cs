using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingBloodPrefab;
    public float secondsBetweenSpawns = 1f;
    float nextSpawnTime;
    public Vector2 spawnSizeMinMax;
    Vector2 screenHalfSizeWorldUnits;
    public Transform[] bloodrain;
    
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }
    void Update()
    {
        if(Time.time > nextSpawnTime){
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            int randomPos = Random.Range(0,bloodrain.Length);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x,screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + .2f);
            fallingBloodPrefab.GetComponent<Transform>().position = spawnPosition;
            GameObject moreBlood = (GameObject)Instantiate(fallingBloodPrefab,bloodrain[randomPos].position,Quaternion.identity);
            moreBlood.transform.localScale = Vector2.one * spawnSize;
        }
        
    }
}
