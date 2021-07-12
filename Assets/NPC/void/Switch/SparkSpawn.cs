using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSpawn : MonoBehaviour {
    public GameObject sparkPrefab;
    public Item powered;
    private void Start() {
        StartCoroutine(SparkSpawnLoop());
    }
    public void SpawnSpark() {
        Instantiate(sparkPrefab, parent: this.gameObject.transform);
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
