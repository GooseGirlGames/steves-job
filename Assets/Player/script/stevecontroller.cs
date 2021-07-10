using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class stevecontroller : MonoBehaviour {

//member variablen
//===================================================================================================================================
    
    public float movement_speed = 100f;
    float horizontal_move = 0f;
    public float jump_height = 3;
    private bool m_facing_right = true; 
    private Vector3 m_velocity = Vector3.zero;
    private Vector3 m_cam_vec = Vector3.zero;
    private float m_old_y = 0f;
    [Range(0, 1f)] [SerializeField] private float m_movement_smoothing = .05f;    // How much to smooth out the movement

    public Animator m_animator;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D m_rigitbody;
    public Transform m_cam;
    [SerializeField] private Transform m_ground_check;
    [SerializeField] private LayerMask ground_layer;
    //public UnityEvent OnLandEvent;
    [SerializeField] private bool is_grounded = false;
    const float k_GroundedRadius = .2f;
    public float crouchspeed = 10f;
    private Transform crouchTransform;
    private float characterHeight; //Initial height
    [SerializeField] private bool crouch = false;
    [SerializeField] private bool always_walking = false;
    public static float CAMERA_OFFSET_Y = 1.4f;
    public float jumpDetectionThreshold = 0.6f;

    private bool movementLocked = false;
    // Whenever Lock() is called, a tag is passed and stored here.
    // Unlock() clears a tag from this list.
    // Only if all lock tags are cleared, Steve will be unlocked.
    private List<string> lockTags = new List<string>();


//Methoden
//===================================================================================================================================


    private void Flip(){
        // Switch the way the player is labelled as facing.
        m_facing_right = !m_facing_right;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Move(float move){
        // the actual movement
        //Debug.Log(move);
        //Debug.Log(m_ridgitbody.velocity.y);

        Vector3 player_velocity = new Vector2(move * 10f, m_rigitbody.velocity.y);
        m_rigitbody.velocity = Vector3.SmoothDamp(m_rigitbody.velocity, player_velocity, ref m_velocity, m_movement_smoothing);
        m_cam_vec.x = m_rigitbody.position.x;
        if (m_old_y > m_rigitbody.position.y){
            m_cam_vec.y = m_rigitbody.position.y;
        } 
        UpdateCameraTarget();

        if (move < 0 && m_facing_right){
            Flip();
        }
        else if (move > 0 && !m_facing_right){
            Flip();
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
            is_grounded = true;
            m_cam_vec.y = m_old_y = m_rigitbody.position.y;
            UpdateCameraTarget();
        }
        //Debug.Log(collision.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
            is_grounded = false; 
        }
        //Debug.Log(collision.collider.gameObject.layer);
        
    }

    
/*     private void Ground_check(){

        Collider2D[] collider = Physics2D.OverlapCircleAll(m_ground_check.position, k_GroundedRadius, ground_layer);
        is_grounded = false;
        Debug.Log(collider.Length);
        if(collider.Length > 0){
            is_grounded = true;
        }
        
    } */

//Start and Update
//===================================================================================================================================

    private void Start(){
        m_rigitbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

    }

    public void Lock(string tag, bool hide = false) {
        if (!lockTags.Contains(tag))
            lockTags.Add(tag);
        horizontal_move = 0f;
        SetAnimatorVars();
        movementLocked = true;
        if (hide) spriteRenderer.enabled = false;
    }


    public void Unlock(string tag) {
        if (lockTags.Contains(tag)) {
            lockTags.Remove(tag);
        }

        if (lockTags.Count == 0) {
            movementLocked = false;
            spriteRenderer.enabled = true;
        }
    }

    private void SetAnimatorVars() {
        m_animator.SetFloat("Speed", Mathf.Abs(horizontal_move));
        m_animator.SetBool("is_grounded", is_grounded);  
        m_animator.SetBool("crouch", crouch);  
    }

    private void Update(){

        // Ignore all input if movement is locked.
        if (movementLocked) {
            SetAnimatorVars();
            return;
        }

        if (PauseMenu.IsPausedOrJustUnpaused()) return;

        //Debug.Log(Time.deltaTime);
        //Debug.Log(horizontal_move);
        //Debug.Log(is_grounded);
        //Debug.Log(crouch);

        if(always_walking == true){
            horizontal_move = movement_speed;
        } else {
            horizontal_move = Input.GetAxis("Horizontal") * movement_speed;
        }

        SetAnimatorVars();
        
        if (Input.GetButton("Crouch") && Mathf.Abs(m_rigitbody.velocity.y) < 0.001f && is_grounded){
            crouch = true; 
            GetComponent<CircleCollider2D>().offset = new Vector2(0.1f,0.05f);  
        } 
        if(Input.GetButtonUp("Jump") && Input.GetButton("Crouch")){
            crouch = true;
        }
        if (Input.GetButtonUp("Crouch")){
            crouch = false; 
        }  
        if (Input.GetButtonDown("Jump") && Mathf.Abs(m_rigitbody.velocity.y) < 0.001f) 
        {   
            crouch = false;
            m_rigitbody.AddForce(new Vector2(0, jump_height), ForceMode2D.Impulse);
            m_cam_vec.x = m_rigitbody.position.x;
            UpdateCameraTarget();  
        }  
        if(!crouch){
            GetComponent<CircleCollider2D>().offset = new Vector2(0.00499999942f,0.195528999f);
        }
        
        // goes up to about 0.45 when Steve jumps
        float cameraTargetDistanceY = Math.Abs(m_rigitbody.position.y - m_cam_vec.y);
        if (cameraTargetDistanceY > jumpDetectionThreshold) {
            SetCameraTargetToPlayer();
        }
    }

    public void SetCameraTargetToPlayer() {
        m_cam_vec = m_rigitbody.position;
        UpdateCameraTarget();
    }

    private void FixedUpdate() {
        //Ground_check();
        Move(horizontal_move * Time.fixedDeltaTime);
        //Debug.Log(horizontal_move);
        //Debug.Log(Time.fixedDeltaTime);
        //Debug.Log(m_ridgitbody.velocity.y);
    }

    private void UpdateCameraTarget() {
        m_cam.transform.position = m_cam_vec + new Vector3(0, CAMERA_OFFSET_Y, 0); 
    }
}
