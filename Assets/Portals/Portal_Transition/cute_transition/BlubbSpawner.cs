using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlubbSpawner : MonoBehaviour {
    public const float MAX_COUNT = 343;
    public GameObject bubblePrefab;
    public RectTransform rect1;
    public RectTransform rect2;

    public void SpawnBubbles() {
        for (int i = 0; i < MAX_COUNT; ++i) {
            Blubb b = Instantiate(bubblePrefab, parent: this.gameObject.transform).GetComponent<Blubb>();
            b.BubbleStart(rect1, rect2);
        }
    }

    public void KillAll() {
        foreach (Blubb b in GameObject.FindObjectsOfType<Blubb>()) {
            b.Kill();
        }
    }

}
