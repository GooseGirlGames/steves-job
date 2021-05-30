using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A Portal that can teleport the player to any destination in any scene.
 * The GameObject must also posess a 2D Collider marked as a Trigger.
 */
public class Portal : MonoBehaviour
{
    public const KeyCode triggerButton = KeyCode.E; 

    public enum TriggerType {
        OnInputPressed,
        Immediate,
        OnlyThroughCode,
    };
    public enum DestinationType {
        SameScene,
        ChangeScene,
    }


    public TriggerType triggerType;
    public DestinationType destinationType;


    public string targetSceneName = "";
    [Tooltip("Position to teleport to.  Also works across scenes, but only makes sense for " +
             "scenes that are aligned (e.g. the malls).  Otherwise, use targetName.")]
    public Transform target = null;
    [Tooltip("If this is specified and destinationType is ChangeScene, teleport the player to " +
            "the GameObject with this name.")]
    public string targetName = "";
    
    [Tooltip("Store pre-transition player position and teleport player there after transition.")]
    public bool teleportAfterSceneChange = false;


    public Animator transitionAnimation;

    [Tooltip("Factor to scale animation speed.")]
    public float animationSpeedFactor = 1.0f;

    [Tooltip("Additional delay, i.e. for how long the screen stays black.")]
    public float animationDelay = 0.0f;

    public SpriteRenderer debugSpriteRenderer = null;

    private const float ANIMATION_DURATION = 0.5f;  // Duration of the animations themselves

    private bool playerInTrigger = false;
    private Vector3 playerPosition;
    private Vector3 playerVelocity;  // may or may not work
    private Vector3? targetPosition = null;  // After scene change, `target` will be null,
                                             // so we need to cache its position

    private void Awake() {
        if (transitionAnimation) {
            transitionAnimation.SetFloat("Speed", animationSpeedFactor);
        }
    }

    public void TriggerTeleport() {
        // store pre-teleport player position
        GameObject player = GameObject.Find("Player");
        playerPosition = player.transform.position;
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        playerVelocity = rigidBody.velocity;

        if (target) {
            targetPosition = target.position;
        }

        StartCoroutine(WaitForTransitionAnimation());
    }

    IEnumerator WaitForTransitionAnimation() {
        if (transitionAnimation) {
            transitionAnimation.SetTrigger("ExitScene");
            // Wait for FadeOut animation plus additional delay
            yield return new WaitForSeconds(
                ANIMATION_DURATION / animationSpeedFactor + animationDelay
            );   
        }

        if (destinationType == DestinationType.ChangeScene) {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(targetSceneName);
        } else if (destinationType == DestinationType.SameScene) {
            GameObject player = GameObject.Find("Player");
            player.transform.position = target.position;
            transitionAnimation.SetTrigger("EnterScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
            GameManager.Instance.hintUI.Hint(this.gameObject.transform, "E");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
            GameManager.Instance.hintUI.ClearHint();
        }

    }

    void Update() {
        if (playerInTrigger) {
            // FIXME Do not hard code keycode
            if (triggerType == TriggerType.OnInputPressed && Input.GetKeyDown(triggerButton)) {
                TriggerTeleport();
            } else if (triggerType == TriggerType.Immediate) {
                TriggerTeleport();
            }
        }

        if (debugSpriteRenderer) {
            debugSpriteRenderer.color = playerInTrigger ? Color.green : Color.magenta;
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (destinationType != DestinationType.ChangeScene) {
            Debug.LogWarning("OnSceneLoaded called even though scene was not changed");
            return;
        }

        GameObject player = GameObject.Find("Player");

        if (teleportAfterSceneChange) {
            player.transform.position = playerPosition;

            Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
            rigidBody.velocity = playerVelocity;
        } else if (targetPosition is Vector3 pos) {
            player.transform.position = pos;
        } else if (targetName != "") {
            GameObject target = GameObject.Find(targetName);
            if (target) {
                player.transform.position = target.transform.position;
            } else {
                Debug.LogWarning("Cannot find teleportatiob target " + targetName);
            }
        }
    }

}
