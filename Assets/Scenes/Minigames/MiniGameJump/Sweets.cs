using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    private int speed;
    private Rigidbody2D candy;
    private Vector2 screenBounds;
    public bool trigger = false;
    public bool notYetTriggered = true;


    public float delay = 4.0f;
    void Start()
    {   
        candy = this.GetComponent<Rigidbody2D>();
        int rand_speed = Random.Range(10, 14);
        speed = rand_speed;
        candy.velocity = new Vector2(-speed, 0);
        Debug.Log(speed);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            trigger = true;
            Debug.Log("HIT");
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            notYetTriggered = false;
        }
    }
    
    void Update()
    {
        float camX = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        if (!notYetTriggered && transform.position.x < camX - 5){
            Destroy(this.gameObject);
        }
        
    }
}
