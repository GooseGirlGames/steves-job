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
    public const KeyCode TRIGGER_BUTTON = KeyCode.E;
    public const KeyCode UP_BUTTON = KeyCode.W;
    public const KeyCode DOWN_BUTTON = KeyCode.S;
    public const float DOOR_ANIMATION_MIN_DISTANCE = 3.0f;

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
    public Transform targetUp = null;
    public Transform targetDown = null;
    private bool elevator = false;
    [Tooltip("If this is specified and destinationType is ChangeScene, teleport the player to " +
            "the GameObject with this name.")]
    public string targetName = "";
    
    [Tooltip("Store pre-transition player position and teleport player there after transition.")]
    public bool teleportAfterSceneChange = false;


    public Animator transitionAnimation;

    public Animator portalAnimator;
    private static bool animateDoorOpening = false;

    [Tooltip("Factor to scale transition animation speed.")]
    public float transitionAnimationSpeedFactor = 1.0f;
    //[Tooltip("Factor to scale door animation speed.")]
    //public float portalAnimationSpeedFactor = 0.6f;

    [Tooltip("Additional delay, i.e. for how long the screen stays black.")]
    public float animationDelay = 0.0f;

    public Transform hintPosition;

    private const float ANIMATION_DURATION = 0.5f;  // Duration of the animations themselves

    private bool playerInTrigger = false;
    public LayerMask playerLayer;
    private Vector3 playerPosition;
    private Vector3 playerVelocity;  // may or may not work
    private Vector3? targetPosition = null;  // After scene change, `target` will be null,
                                             // so we need to cache its position
    private Coroutine destinationHintCoroutine = null;

    private void Awake() {
        if (transitionAnimation) {
            transitionAnimation.SetFloat("Speed", transitionAnimationSpeedFactor);
        }
        //if (portalAnimator) {
        //    portalAnimator.SetFloat("Speed", portalAnimationSpeedFactor);
        //}
        elevator = (targetDown != null) || (targetUp != null);
    }

    public void TriggerTeleport() {

        // for same-scene teleports, we only want to update the hint, not clear it
        if (destinationType != DestinationType.SameScene) {
            GameManager.Instance.hintUI.ClearHint();
        } 
        
        // store pre-teleport player position
        GameObject player = GameObject.Find("Player");
        playerPosition = player.transform.position;
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        playerVelocity = rigidBody.velocity;
        animateDoorOpening = true;

        if (target) {
            targetPosition = target.position;
        }
        // for elevators, targetPosition is set elsewhere

        StartCoroutine(WaitForTransitionAnimation());
    }

    IEnumerator WaitForTransitionAnimation() {
        if (transitionAnimation) {
            transitionAnimation.SetTrigger("ExitScene");
            // Wait for FadeOut animation plus additional delay
            yield return new WaitForSeconds(
                ANIMATION_DURATION / transitionAnimationSpeedFactor + animationDelay
            );   
        }

        if (destinationType == DestinationType.ChangeScene) {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(targetSceneName);
        } else if (destinationType == DestinationType.SameScene) {
            GameObject player = GameObject.Find("Player");
            if (targetPosition is Vector3 v) {
                player.transform.position = v;
            } else {
                player.transform.position = target.position;
            }
            
            if (transitionAnimation) {
                transitionAnimation.SetTrigger("EnterScene");
            }
        }
    }

    IEnumerator WaitWithDestinationHint() {
        yield return new WaitForSeconds(2);
        HintDestination();
        yield return new WaitForSeconds(TargetCamera.IN_TRANSITION_ANIMATION_DURATION);
        yield return new WaitForSeconds(1);
        TargetCamera.Disable();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
            if (destinationHintCoroutine == null) {
                destinationHintCoroutine = StartCoroutine(WaitWithDestinationHint());
            }
        }
    }

    void HintKeyPress() {
        if (triggerType == TriggerType.OnInputPressed) {
            if (elevator) {
                string hint = "W\nS";
                if (targetUp && !targetDown) hint = "W";
                if (!targetUp && targetDown) hint = "S";
                GameManager.Instance.hintUI.Hint(hintPosition, hint);
            } else {
                GameManager.Instance.hintUI.Hint(hintPosition, "E");
            }
        }
    }

    void HintDestination() {
        if (destinationType == DestinationType.SameScene) {
            if (elevator) {
                if (targetUp && !targetDown) TargetCamera.Target(targetUp);
                if (!targetUp && targetDown) TargetCamera.Target(targetDown);
            } else {
                TargetCamera.Target(target);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
            GameManager.Instance.hintUI.ClearHint();
            animateDoorOpening = false;
            if (destinationHintCoroutine != null) {
                StopCoroutine(destinationHintCoroutine);
                destinationHintCoroutine = null;
            }
            TargetCamera.Disable();
        }
    }

    void Update() {
        if (PauseMenu.IsPausedOrJustUnpaused()) return;

        Collider2D[] collidersNearby = Physics2D.OverlapCircleAll(
                gameObject.transform.position,
                DOOR_ANIMATION_MIN_DISTANCE,
                playerLayer
        );
        bool playerNearby = collidersNearby.Length > 0;

        if (portalAnimator) {
            portalAnimator.SetBool("PlayerNearby", playerNearby);
            portalAnimator.SetBool("PlayerJustTeleported", animateDoorOpening);
        }

        if (playerInTrigger) {
            // FIXME Do not hard code keycode
            if (elevator) {
                if (targetUp && Input.GetKeyDown(UP_BUTTON)) {
                    targetPosition = targetUp.transform.position;
                    TriggerTeleport();
                }
                if (targetDown && Input.GetKeyDown(DOWN_BUTTON)) {
                    targetPosition = targetDown.transform.position;
                    TriggerTeleport();
                }
            } else if (triggerType == TriggerType.OnInputPressed && Input.GetKeyDown(TRIGGER_BUTTON)) {
                TriggerTeleport();
            } else if (triggerType == TriggerType.Immediate) {
                TriggerTeleport();
            }
        }

        if (playerInTrigger) {
            if (!GameManager.Instance.hintUI.IsHinting()) {
                HintKeyPress();
            }
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
