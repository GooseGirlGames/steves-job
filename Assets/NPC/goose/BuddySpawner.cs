using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuddySpawner : MonoBehaviour {
    private stevecontroller following;
    private GameObject buddy = null;
    private Animator buddyAnimator;
    private Renderer buddyRenderer;
    public GameObject buddyPrefab;
    private float delaySeconds = 0.8f;
    private Queue<SteveState> states = new Queue<SteveState>();
    private Vector3 localScaleLeft;
    private Vector3 localScaleRight;
    public Item goose;
    
    void Start() {
        if (SceneManager.GetActiveScene().name == "JumpMiniGame") {
            return;
        }
        following = GetComponent<stevecontroller>();
        buddy = Instantiate(buddyPrefab);
        buddyAnimator = buddy.GetComponentInChildren<Animator>();
        buddyRenderer = buddy.GetComponentInChildren<SpriteRenderer>();
        localScaleLeft = buddyRenderer.transform.localScale;
        localScaleRight = buddyRenderer.transform.localScale;
        localScaleRight.x *= -1;
    }

    void Update() {
        if (buddy == null) {
            return;
        }

        buddyRenderer.enabled = Inventory.Instance.HasItem(goose);

        states.Enqueue(following.GetMovementState());
        
        var state = states.Peek();
        buddy.transform.position = state.pos;
        buddyAnimator.SetFloat("Speed", state.speed);
        buddyAnimator.SetBool("is_grounded", state.grounded);  
        buddyAnimator.SetBool("crouch", state.crouch);

        buddyRenderer.transform.localScale =
                state.facing_right ? localScaleRight : localScaleLeft;

        if (states.Count > 60 * delaySeconds) {
            states.Dequeue();
        }
    }
}
