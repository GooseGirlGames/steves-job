using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweets : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D candy;
    private Vector2 screenBounds;
    public bool trigger = false;
    public bool wasTriggered = true;


    public float delay = 4.0f;
    void Start()
    {   
        candy = this.GetComponent<Rigidbody2D>();
        candy.velocity = new Vector2(-speed, 0);
    }
 
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            trigger = true;
            //Debug.Log("HIT");
        }
    }
    
    void Update()
    {
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if(transform.position.x < screenBounds.x - 45){
            Destroy(this.gameObject);
        }
    }
}
