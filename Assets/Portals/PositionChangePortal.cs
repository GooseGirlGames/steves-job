using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Usage: Create an empty object, add the SceneLoader component and
 * a 2D collider.  Set "Target Scene Name" of the SceneLoader
 * and tick "Is Trigger" for the collider.
 */
public class PositionChangePortal : MonoBehaviour
{

    public enum SceneLoaderType {
        OnInputPressed,
        Immediate,
    };

    [SerializeField]
    public SceneLoaderType Type = SceneLoaderType.OnInputPressed;

    [SerializeField]
    public Transform target;

    [SerializeField]
    public SpriteRenderer debugSpriteRenderer = null;

    [SerializeField]
    public bool PreservePlayerPosition = true;

    [SerializeField]
    public Animator transitionAnimation;

    [Tooltip("Factor to scale animation speed.")]
    public float animationSpeedFactor = 1.0f;

    [Tooltip("Additional delay, i.e. for how long the screen stays black.")]
    public float animationDelay = 0.0f;
    /** Duration of the animations themselves */
    private const float ANIMATION_DURATION = 0.5f;

    private bool playerInTrigger = false;
    private Vector3 playerPosition;
    private Vector3 playerVelocity;  // FIXME velocity still ends up at 0 for some reason

    private void Awake() {
        if (transitionAnimation) {
            transitionAnimation.SetFloat("Speed", animationSpeedFactor);
        }
    }

    public void TriggerSceneLoad() {
        if (PreservePlayerPosition) {
            GameObject playerPreLoad = GameObject.Find("Player");
            playerPosition = playerPreLoad.transform.position;
            Rigidbody2D rigidBody = playerPreLoad.GetComponent<Rigidbody2D>();
            playerVelocity = rigidBody.velocity;
        }

        StartCoroutine(WaitForSceneLoadAnimation());
    }

    IEnumerator WaitForSceneLoadAnimation() {
        if (transitionAnimation) {
            transitionAnimation.SetTrigger("ExitScene");
            // Wait for FadeOut animation plus additional delay
            yield return new WaitForSeconds(ANIMATION_DURATION / animationSpeedFactor + animationDelay);   
        }

        GameObject player = GameObject.Find("Player");
        player.transform.position = target.position;
        OnSceneLoaded();
        transitionAnimation.SetTrigger("EnterScene");
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

    void OnSceneLoaded() {
        if (PreservePlayerPosition) {
            GameObject playerPostLoad = GameObject.Find("Player");
            playerPostLoad.transform.position = playerPosition;

            Rigidbody2D rigidBody = playerPostLoad.GetComponent<Rigidbody2D>();
            rigidBody.velocity = playerVelocity;
        }
    }

}
