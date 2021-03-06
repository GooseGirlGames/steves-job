using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSpawn : MonoBehaviour {
    public GameObject sparkPrefab;
    public Item powered;
    private AudioSource sparkle = null;
    private void Start() {
        StartCoroutine(SparkSpawnLoop());
        sparkle = GetComponent<AudioSource>();
    }
    public void SpawnSpark() {
        Instantiate(sparkPrefab, parent: this.gameObject.transform);
        if(sparkle) sparkle.Play();
    }

    private IEnumerator SparkSpawnLoop() {
        while (true) {
            if (Inventory.Instance.HasItem(powered)) {
                SpawnSpark();
            }
            yield return new WaitForSeconds(Random.Range(0.6f, 1f));
        }
    }
}
