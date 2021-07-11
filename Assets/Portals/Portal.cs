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
    public const string TRIGGER_BUTTON_NAME = "Submit";
    public const string TRIGGER_BUTTON_STR = "E";
    public const float DOOR_ANIMATION_MIN_DISTANCE = 3.0f;
    public const float ELEVATOR_ANIMATION_DURATION = 3.0f;

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
    [SerializeField] private bool elevator = false;
    [Tooltip("If this is specified and destinationType is ChangeScene, teleport the player to " +
            "the GameObject with this name.")]
    public string targetName = "";
    
    [Tooltip("Store pre-transition player position and teleport player there after transition.")]
    public bool teleportAfterSceneChange = false;
    private SceneTransitionManagement transitionAnimationManager;
    public Animator portalAnimator;
    private static bool animateDoorOpening = false;

    [Tooltip("Factor to scale transition animation speed.")]
    public float transitionAnimationSpeedFactor = 1.0f;
    //[Tooltip("Factor to scale door animation speed.")]
    //public float portalAnimationSpeedFactor = 0.6f;

    [Tooltip("Additional delay, i.e. for how long the screen stays black.")]
    public float animationDelay = 0.0f;
    public float animationDelayChangeScene = 2.0f;
    public bool animateTransition = true;

    public Transform hintPosition;
    public bool animateAsElevator = false;

    private const float ANIMATION_DURATION = 0.5f;  // Duration of the animations themselves

    private bool playerInTrigger = false;
    public LayerMask playerLayer;
    public World targetWorld;
    private Vector3 playerPosition;
    private Vector3 playerVelocity;  // may or may not work
    private Vector3? targetPosition = null;  // After scene change, `target` will be null,
                                             // so we need to cache its position
    private Coroutine destinationHintCoroutine = null;
    private Coroutine elevatorAnimationCoroutine = null;

    private void Awake() {
        if (animateTransition) {
            transitionAnimationManager = GameObject.FindObjectOfType<SceneTransitionManagement>();
        }
        
        if (destinationType == DestinationType.ChangeScene) {
            animationDelay = animationDelayChangeScene;
        }
        //if (portalAnimator) {
        //    portalAnimator.SetFloat("Speed", portalAnimationSpeedFactor);
        //}
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

        if (elevator && animateAsElevator) {
            if (elevatorAnimationCoroutine == null) {
                elevatorAnimationCoroutine = StartCoroutine(ElevatorAnimation());
            }
        } else {
             StartCoroutine(WaitForTransitionAnimation());
        }
    }

    IEnumerator WaitForTransitionAnimation() {
        if (transitionAnimationManager && transitionAnimationManager.black) {
            transitionAnimationManager.black.SetFloat("Speed", transitionAnimationSpeedFactor);
            transitionAnimationManager.black.SetTrigger("ExitScene");
            if (targetWorld != World.Normal) {  // fancy animation, let's goooooooooooooo
                Animator fancy = SceneTransitionManagement.Instance.GetAnimator(targetWorld);
                Animator steve = SceneTransitionManagement.Instance.GetSteveAnimator(targetWorld);
                yield return new WaitForSeconds(
                    ANIMATION_DURATION / transitionAnimationSpeedFactor
                );
                if (steve) {
                    Debug.Log("Stev!" + steve);
                    steve.SetTrigger("Wheeeeee");
                }
                if (fancy) {

                    BlubbSpawner bubbleSpawner = null;
                    if (targetWorld == World.Cute) {
                        bubbleSpawner = GameObject.FindObjectOfType<BlubbSpawner>();
                    }

                    if (bubbleSpawner) bubbleSpawner.SpawnBubbles();

                    Debug.Log("Fancy!" + fancy);
                    fancy.SetTrigger("FadeIn");
                    yield return new WaitForSeconds(animationDelay - (1.1f * Blubb.POPP_TIME));
                    if (bubbleSpawner) bubbleSpawner.KillAll();
                    yield return new WaitForSeconds(1.1f * Blubb.POPP_TIME);

                    fancy.SetTrigger("FadeOut");
                }
            }
            
            // Wait for FadeOut animation plus additional delay
            yield return new WaitForSeconds(
                ANIMATION_DURATION / transitionAnimationSpeedFactor
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
            if (elevator) player.transform.position -= new Vector3(0, stevecontroller.CAMERA_OFFSET_Y, 0);
            
            if (transitionAnimationManager && transitionAnimationManager.black) {
                transitionAnimationManager.black.SetTrigger("EnterScene");
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

    IEnumerator ElevatorAnimation() {
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        player.Lock(tag: "elevator", hide: true);
        HintDestination(ELEVATOR_ANIMATION_DURATION);

        
        yield return new WaitForSeconds(ELEVATOR_ANIMATION_DURATION * 0.9f);
        StartCoroutine(WaitForTransitionAnimation());
        yield return new WaitForSeconds(ELEVATOR_ANIMATION_DURATION * 0.1f);
        
        TargetCamera.SetBlendTimeOverride(1f);
        TargetCamera.Disable();
        player.Unlock(tag: "elevator");
        elevatorAnimationCoroutine = null;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
            if (destinationHintCoroutine == null) {
                //destinationHintCoroutine = StartCoroutine(WaitWithDestinationHint());
            }
        }
    }

    void HintKeyPress() {
        if (triggerType == TriggerType.OnInputPressed) {
            GameManager.Instance.hintUI.Hint(hintPosition, "E");
        }
    }

    void HintDestination(float? blendTime = null) {
        if (destinationType == DestinationType.SameScene) {
            TargetCamera.Target(target, blendTime);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
            GameManager.Instance.hintUI.ClearHint();
            animateDoorOpening = false;
            if (elevatorAnimationCoroutine == null) {
                if (destinationHintCoroutine != null) {
                    StopCoroutine(destinationHintCoroutine);
                    destinationHintCoroutine = null;
                }
                TargetCamera.Disable();
            }
            
        }
    }

    void Update() {
        if (PauseMenu.IsPausedOrJustUnpaused() || InventoryCanvasSlots.Instance.IsShowing()) {
            return;
        }

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
            if (triggerType == TriggerType.OnInputPressed && Input.GetButtonDown(TRIGGER_BUTTON_NAME)) {
                TriggerTeleport();
            } else if (triggerType == TriggerType.Immediate) {
                TriggerTeleport();
            }
        }

        if (playerInTrigger) {
            if (!GameManager.Instance.hintUI.IsHinting() && elevatorAnimationCoroutine == null) {
                HintKeyPress();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if (destinationType != DestinationType.ChangeScene) {
            Debug.LogWarning("OnSceneLoaded called even though scene was not changed");
            return;
        }

        //Animator transitionAnimation = SceneTransitionManagement.Instance.GetAnimator();
        //if (transitionAnimation) {
        //    transitionAnimation.SetTrigger("EnterScene");
        //}

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
                //                                        ^ This typo is canon now.  I am not sorry.
            }
        }

        GameManager.Instance.hintUI.ClearHint();
    }

}
