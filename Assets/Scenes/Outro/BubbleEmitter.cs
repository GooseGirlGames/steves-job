using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEmitter : MonoBehaviour {
    public GameObject bubblePrefab;
    void Start() {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn() {
        yield return new WaitForSeconds(12);
        for (int i = 0; i < 16; ++i) {
            Instantiate(bubblePrefab, this.gameObject.transform);
            yield return new WaitForSeconds(0.25f);
        }
        for (int i = 0; i < 300; ++i) {
            Instantiate(bubblePrefab, this.gameObject.transform);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
