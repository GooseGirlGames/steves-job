using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blubb : MonoBehaviour {
    public const float KILL_DISTANCE = 7000.0f;
    public const float SPAWN_BOX_SIZE = 2500.0f;
    public RectTransform rect;
    public Image image;
    private Vector3 direction_normalized;
    private float velocity;
    private float timeToLive;
    public List<Sprite> intactBubbles;
    public List<Sprite> poppedBubbles;
    private int spriteIdx;

    void Start() {
        spriteIdx = Random.Range(0, intactBubbles.Count - 1);
        image.sprite = intactBubbles[spriteIdx];

        rect.position = RandPos();
        float angle = Random.Range(0.0f, 2 * Mathf.PI);
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        direction_normalized = Vector3.Normalize(direction);
        velocity = Random.Range(1.0f, 5.0f);

        float hue = Random.Range(0.0f, 1.0f);
        image.color = Color.HSVToRGB(hue, 1, 1);

        timeToLive = Random.Range(0.5f, 2.0f);
    }

    // Update is called once per frame
    void Update() {
        rect.position = rect.position + (velocity * direction_normalized);

        float distanceFromOrigin = Mathf.Abs(Vector2.Distance(Vector2.zero, rect.position));
        if (timeToLive < 0.1f) {
            image.sprite = poppedBubbles[spriteIdx];
        }
        if (distanceFromOrigin > KILL_DISTANCE || timeToLive < 0.0f) {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        timeToLive -= Time.fixedDeltaTime;
    }

    private static Vector2 RandPos() {
        float x = Random.Range(-SPAWN_BOX_SIZE, SPAWN_BOX_SIZE);
        float y = Random.Range(-SPAWN_BOX_SIZE, SPAWN_BOX_SIZE);
        return new Vector2(x, y);
    }
}
