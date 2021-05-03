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

    public enum SceneLoaderType {
        OnInputPressed,
        Immediate,
    };

    [SerializeField]
    public SceneLoaderType Type = SceneLoaderType.OnInputPressed;

    [SerializeField]
    public string targetSceneName = "";

    [SerializeField]
    public SpriteRenderer debugSpriteRenderer = null;

    private bool playerInTrigger = false;
    private Vector3 playerPosition;
    private Vector3 playerVelocity;  // FIXME velocity still ends up at 0 for some reason

    void TriggerSceneLoad() {
        GameObject playerPreLoad = GameObject.Find("Player");
        playerPosition = playerPreLoad.transform.position;
        Rigidbody2D rigidBody = playerPreLoad.GetComponent<Rigidbody2D>();
        playerVelocity = rigidBody.velocity;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(targetSceneName);



    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
            if (Type == SceneLoaderType.Immediate) {
                TriggerSceneLoad();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
        }

    }

    void Update() {
        // FIXME Do not hard code keycode
        if (Type == SceneLoaderType.OnInputPressed && playerInTrigger && Input.GetKeyDown(KeyCode.E)) {
            TriggerSceneLoad();
        }
        debugSpriteRenderer.color = playerInTrigger ? Color.green : Color.magenta;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject playerPostLoad = GameObject.Find("Player");
        playerPostLoad.transform.position = playerPosition;

        Rigidbody2D rigidBody = playerPostLoad.GetComponent<Rigidbody2D>();
        rigidBody.velocity = playerVelocity;
    }

}
