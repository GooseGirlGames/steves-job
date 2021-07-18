using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class BlubbVomit : MonoBehaviour {
    public const float KILL_DISTANCE = 700.0f;
    new private Transform transform;
    private SpriteRenderer image;
    private Vector3 direction_normalized;
    private float velocity;
    private float timeToLive;
    public List<Sprite> intactBubbles;
    public List<Sprite> poppedBubbles;
    private int spriteIdx;
    public const float POPP_TIME = 0.06f;
    private float sideways_force = 0.0f;
    public const float SIDEWAYS_WEIGHT = 0.01f;
    private Vector3 startPos;
    new public Light2D light;

    void Start() {
        transform = GetComponent<Transform>();
        image = GetComponent<SpriteRenderer>();
        startPos = transform.position;


        sideways_force = Random.Range(-4f, 2.5f);

        spriteIdx = Random.Range(0, intactBubbles.Count - 1);
        image.sprite = intactBubbles[spriteIdx];

        float angle = 2 * Mathf.PI * (105f + Random.Range(0f, 0f)) / 360.0f;
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        direction_normalized = Vector3.Normalize(direction);
        velocity = Random.Range(0.15f, 0.3f);

        float hue = Random.Range(0.0f, 1.0f);
        image.color = Color.HSVToRGB(hue, 1, 1);
        light.color = image.color;

        timeToLive = Random.Range(1.5f, 3.0f);
    }

    // Update is called once per frame
    void Update() {
        float distanceFromOrigin = Mathf.Abs(Vector2.Distance(startPos, transform.position));

        var sideways = Mathf.Sqrt(distanceFromOrigin) * new Vector3(0, sideways_force, 0);

        transform.position =
                transform.position
                + (velocity * direction_normalized)
                + (sideways * SIDEWAYS_WEIGHT);

        if (timeToLive < POPP_TIME) {
            image.sprite = poppedBubbles[spriteIdx];
            velocity = 0;
        }
        if (distanceFromOrigin > KILL_DISTANCE || timeToLive < 0.0f) {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        timeToLive -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Jeremias")) {
            Kill();
        }
    }
    public void Kill() {
        timeToLive = POPP_TIME;
        velocity = 0;
    }
}
