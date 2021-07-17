using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCameraScroll : MonoBehaviour {
    private float speed = 0.1f; 
    private void FixedUpdate() {
        var delta = new Vector3(0, -1, 0) * speed;
        transform.position = transform.position + delta;
    }
}
