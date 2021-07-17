using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroWaddle : MonoBehaviour
{
    private float speed = 8f;
    public const float FIONN_THRES = 13f;
    
    private void FixedUpdate() {
        //Debug.Log(transform.position.x);
        if (transform.position.x < FIONN_THRES) {
            transform.position = transform.position + (new Vector3(1, 0, 0) * speed * Time.fixedDeltaTime);
        } else {
            GetComponent<Animator>().SetTrigger("Stop");
        }
    }
}
