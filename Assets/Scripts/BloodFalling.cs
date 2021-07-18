using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BloodFalling : MonoBehaviour {
    public Portal loserPortal;
    public Progressbar progressbar;
    new Rigidbody2D rigidbody2D;
    public Sprite splatter;
    private Vector2 screenbounds;
    public Item lostItem;
    public Item winItem;
    private AudioSource audioSource;

    public static int splatCount;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        //progressbar.minBlood(counter);
        //rigidbody2D = this.GetComponent<Rigidbody2D> ();
        screenbounds = Camera.main.ScreenToWorldPoint(transform.position);
    }

    void onTriggerEnter2D (Collider2D collider2D) {
        if (collider2D.gameObject.name.Equals("Player"))
        {
            rigidbody2D.isKinematic = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        if(collision2D.gameObject.name.Equals("Background")){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = splatter;
            this.gameObject.tag = "splatter";   
            audioSource.Play();         
            Debug.Log("R.I.P");
        }
        else if(collision2D.gameObject.name.Equals("Player") && this.gameObject.transform.position.y > -3.8f){
            Destroy(this.gameObject);
            Debug.Log("HELL YEAH");
        }
        else if(collision2D.gameObject.name.Equals("blood(Clone)")){
            this.gameObject.tag = "splatter";
            audioSource.Play();
        }
        Debug.Log(collision2D.gameObject.name);

    }

    void OnEnable(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag("splatter");
        foreach(GameObject obj in objects){
            Physics2D.IgnoreCollision(obj.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
        }
    }

    void GameLost(){
        if (!Inventory.Instance.HasItem(winItem)) {
            Inventory.Instance.AddItem(lostItem);
        }
        DialogueManager.Instance.SetInstantTrue();
        loserPortal.TriggerTeleport();
        
        //Debug.Log("bucket");
    }

    void Update(){
        GameObject[] objectsSplat = GameObject.FindGameObjectsWithTag("splatter");
        int countSplat = objectsSplat.Length;

        BloodFalling.splatCount = countSplat;
        
        if(countSplat == 3){
            GameLost();
            BloodFalling.splatCount = 4;
            Debug.Log("Game Over");
        }
    }
    
    
}

