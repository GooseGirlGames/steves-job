using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Magpie : MonoBehaviour {
    public Transform keyTarget;
    public List<Transform> targetPositions;
    private Transform currentTarget = null;
    private Vector3 startPos;
    private float speed = 0.17f;
    private float distanceTarget = float.PositiveInfinity;
    private float totalDistance;
    public SpriteRenderer magpieRenderer;
    public SpriteRenderer keyRenderer;
    public SpriteRenderer keyInWorldRenderer;
    private stevecontroller player;
    private int targetIdx = 0;
    public Item _magpie_released;
    public Item _magpie_taunting;
    private bool taunting = false;
    private Animator animator;
    private bool facingRight = false;
    private void Awake() {
        animator = GetComponent<Animator>();
        keyRenderer.enabled = false;
        magpieRenderer.enabled = false;
        player = GameObject.FindObjectOfType<stevecontroller>();
        if (Inventory.Instance.HasItem(_magpie_released)) {
            Spawn();
        }
    }
    public void Spawn() {
        taunting = Inventory.Instance.HasItem(_magpie_taunting);
        if (taunting) {
            keyRenderer.enabled = true;
            speed *= 1000.0f;
            targetIdx = targetPositions.Count - 1;
            SetTarget(targetPositions[targetIdx]);
            speed /= 1000.0f;
        } else {
            keyRenderer.enabled = false;
            SetTarget(keyTarget);
        }
        magpieRenderer.enabled = true;
    }
    public void GetEaten() {
        keyRenderer.enabled = false;
        magpieRenderer.enabled = false;
    }
    private void SetTarget(Transform target) {
        startPos = gameObject.transform.position;
        currentTarget = target;
        totalDistance = Vector2.Distance(target.position, startPos);
    }
    private void NewTarget() {
        if (taunting) {
            return;
        }

        if (currentTarget == keyTarget) {
            keyRenderer.enabled = true;
            keyInWorldRenderer.enabled = false;
            SetTarget(targetPositions[0]);
            targetIdx = 0;
        } else {
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            if (distanceToPlayer < 3.4f) {
                if (++targetIdx < targetPositions.Count) {
                    SetTarget(targetPositions[targetIdx]);
                } else {
                    Inventory.Instance.AddItem(_magpie_taunting);
                    taunting = true;
                }
            }
        }
    }
    private void Move() {
        Vector3 direction = currentTarget.position - gameObject.transform.position;
        Vector3 direction_norm = Vector3.Normalize(direction);
        float progress = distanceTarget / totalDistance;
        float ease = (Mathf.Sin(progress * Mathf.PI) + 1) / 2;
        Vector3 moveDelta = direction_norm * speed * ease;
        gameObject.transform.position += moveDelta;
        animator.SetFloat("Speed", 2 - ((Vector2) moveDelta).magnitude);

        bool wasFacingRight = facingRight;
        facingRight = moveDelta.x > 0;
        if ((wasFacingRight && !facingRight) || (!wasFacingRight && facingRight)) {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
        }
    }
    private void FixedUpdate() {
        if(currentTarget == null) return;

        distanceTarget = Vector2.Distance(currentTarget.position, gameObject.transform.position);
        if (distanceTarget > 0.1f) {
            Move();
        } else {
            animator.SetFloat("Speed", 0);
            NewTarget();
        }
    }
   
}
