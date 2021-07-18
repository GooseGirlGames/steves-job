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
    public float delaySeconds = 0.8f;
    private Queue<SteveState> states = new Queue<SteveState>();
    private Vector3 localScaleLeft;
    private Vector3 localScaleRight;
    public Item goose;
    public RuntimeAnimatorController goose_ctrl;
    public Item goose_cute;
    public RuntimeAnimatorController goose_cute_ctrl;
    public Item goose_blood;
    public RuntimeAnimatorController goose_blood_ctrl;
    public Item goose_both;
    public RuntimeAnimatorController goose_both_ctrl;
    
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

        if (Inventory.Instance.HasItem(goose)) {
            buddyAnimator.runtimeAnimatorController = goose_ctrl;
            buddyRenderer.enabled = true;
        } else if (Inventory.Instance.HasItem(goose_cute)) {
            buddyAnimator.runtimeAnimatorController = goose_cute_ctrl;
            buddyRenderer.enabled = true;
        } else if (Inventory.Instance.HasItem(goose_blood)) {
            buddyAnimator.runtimeAnimatorController = goose_blood_ctrl;
            buddyRenderer.enabled = true;
        } else if (Inventory.Instance.HasItem(goose_both)) {
            buddyAnimator.runtimeAnimatorController = goose_both_ctrl;
            buddyRenderer.enabled = true;
        } else {
            buddyRenderer.enabled = false;
        }

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

    public RuntimeAnimatorController Goose() {
        return buddyAnimator.runtimeAnimatorController;
    }
    
    public void CenterBuddy() {
        foreach(var t in buddy.GetComponentsInChildren<Transform>()) {
            t.position = Vector3.zero;
        }
    }
}
