using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweets : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D candy;
    private Vector2 screenBounds;
    public Transform player;
    void Start()
    {
        candy = this.GetComponent<Rigidbody2D>();
        candy.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(screenBounds.x);
        if(transform.position.x < screenBounds.x){
            Destroy(this.gameObject);
        }
    }
}
