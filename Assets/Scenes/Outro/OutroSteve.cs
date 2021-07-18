using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class OutroSteve : MonoBehaviour {
    private float hue = 0f;
    new public Light2D light;
    private float rotSpeed = 145f;
    private float speed = 10f;
    private float delay = 18f;
    public Transform target;

    void Start() {
        StartCoroutine(Disco());
    }
    private IEnumerator Disco() {
        while(true) {
            hue += Random.Range(0.2f, 0.3f);
            if (hue > 1) hue -= 1;
            light.color = Color.HSVToRGB(hue, 1, 1);
            yield return new WaitForSeconds(0.65f);
        }
    }

    void FixedUpdate() {
        delay -= Time.fixedDeltaTime;
        if (delay < 0 && transform.position.x < target.position.x) {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 1) * Time.fixedDeltaTime * rotSpeed);
            this.gameObject.transform.position
                    = this.gameObject.transform.position
                    + new Vector3(1, 0, 0) * speed * Time.fixedDeltaTime;
        }
    }
}
