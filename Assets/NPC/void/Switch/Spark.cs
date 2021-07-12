using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
    public Rigidbody2D rb;
    private float timeToLive = 1.6f;
    void Start() {
        transform.position = GetComponentInParent<Transform>().position;
        rb.AddForce(3.5f * new Vector2(Random.Range(-0.3f, 0.3f), 1), ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        timeToLive -= Time.fixedDeltaTime;
        if (timeToLive < 0.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            timeToLive = 0.2f;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
}
