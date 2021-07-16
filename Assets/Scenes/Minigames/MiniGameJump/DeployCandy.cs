using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCandy : MonoBehaviour
{
    public Portal Portal;

    public GameObject racoon;
    private Vector3 racoon_pos;
    private float racoon_near = 10f;
    private Vector3 player_pos;
    public GameObject[] candyPrefabs;
    public Transform spawn_low;
    public Transform spawn_high;
    private Vector3 spawn_pos;
    private Vector2 candyPrefab_pos;
    private GameObject spawn;

    private GameObject bar;
    private Vector3 moveBar;

    public int respawnTime = 1;
    public int winSeconds;
    private Vector2 screenBounds;
    private Vector2 bounds;
    public Transform player;
    public GameObject stop;
    public Item _miniRacoonGamePlayed;
    public Item _miniRacoonGameWon;
    public Item _racoonMad;
    public Item _storeowner_later;
    public Item _talkToRaccoon;
    
    [SerializeField] private HealthBar healthbar;
    private float health;
    public float raccApproachSpeed = 0.15f;
    private Coroutine spawnCoroutine = null;
    public SpriteRenderer raccoonRenderer;
    public Animator raccoonAnimator;

    public Sprite raccoonHighThrowSprite;
    public Sprite raccoonLowThrowSprite;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spawnCoroutine = StartCoroutine(timedSpawn());
        bar = GameObject.Find("Healthbar");
        health = 1f;
        healthbar.SetSize(health);
        healthbar.SetColour(Color.green);
        winSeconds = respawnTime*15;
    }

    private void spawnCandy(bool high) {
        Transform spawn_transform = high ? spawn_high : spawn_low;
        int prefabIdx = Random.Range(0,candyPrefabs.Length);
        var candyPrefab_pos = new Vector2(spawn_transform.position.x, spawn_transform.position.y); 
        var candyPrefab = candyPrefabs[prefabIdx];
        spawn = Instantiate(candyPrefab, candyPrefab_pos, Quaternion.identity) as GameObject;   
    }


    IEnumerator timedSpawn(){
        while(true){
            yield return new WaitForSeconds(Random.Range(3.0f,4.0f));
            bool high = Random.Range(0.0f, 1.0f) > 0.5;
            raccoonAnimator.SetTrigger(high ? "ThrowHigh" : "ThrowLow");

            // Wait for correct throw sprite
            while (raccoonRenderer.sprite != raccoonHighThrowSprite
                   && raccoonRenderer.sprite != raccoonLowThrowSprite) {
                       yield return new WaitForEndOfFrame();
            }
            spawnCandy(high);
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


    public void GameLost(){
        Debug.Log("Lost Jump MiniGame");

        DialogueManager.Instance.SetInstantTrue();
        
        Inventory.Instance.AddItem(_miniRacoonGamePlayed);
        Inventory.Instance.RemoveItem(_racoonMad);
        Inventory.Instance.RemoveItem(_storeowner_later);

        Portal.TriggerTeleport();
    }
    public void GameWon(){
        Debug.Log("Won Jump MiniGame");

        DialogueManager.Instance.SetInstantTrue();

        Inventory.Instance.AddItem(_miniRacoonGameWon);
        Inventory.Instance.RemoveItem(_racoonMad);
        Inventory.Instance.RemoveItem(_miniRacoonGamePlayed);
        Inventory.Instance.RemoveItem(_storeowner_later);
        Inventory.Instance.RemoveItem(_talkToRaccoon);

        Portal.TriggerTeleport();
    }
    public void FixedUpdate(){
        
        float speedUp = 0.75f;
        if (racoon_near < 5.0f) {
            // Let Steve catch up
            speedUp = 7.0f;
            StopCoroutine(spawnCoroutine);
            healthbar.gameObject.SetActive(false);
        }
        racoon_near -= raccApproachSpeed * Time.deltaTime * speedUp;
        if (racoon_near < 1f) {
            GameWon();
        }

        if(spawn != null){
            if(spawn.GetComponent<Sweets>().notYetTriggered){
                if(spawn.GetComponent<Sweets>().trigger){
                    health -= .15f; 
                    //player_pos = new Vector3(player.position.x - 1.0f, player.position.y, player.position.z);
                    //player.position = player_pos;
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
        if(health <= 0.02f){
            GameLost();
        } 

        racoon_pos = new Vector3(player.position.x + racoon_near, racoon.transform.position.y, 1);
        racoon.transform.position = racoon_pos;
        
        
        moveBar = new Vector3(player.position.x, player.position.y + 0.8f, 1);
        bar.transform.position = moveBar;

    }


}
