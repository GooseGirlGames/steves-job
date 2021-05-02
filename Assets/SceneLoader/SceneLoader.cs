using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Usage: Create an empty object, add the SceneLoader component and
 * a 2D collider.  Set "Target Scene Name" of the SceneLoader
 * and tick "Is Trigger" for the collider.
 */
public class SceneLoader : MonoBehaviour
{

    [SerializeField]
    public string targetSceneName = "";

    void TriggerSceneLoad() {
        SceneManager.LoadScene(targetSceneName);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            TriggerSceneLoad();
        }
    }
}
