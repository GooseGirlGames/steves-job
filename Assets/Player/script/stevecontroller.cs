using UnityEngine;
using UnityEngine.Events;
using System;

public class stevecontroller : MonoBehaviour {

//member variablen
//===================================================================================================================================
    
    public float movement_speed = 100f;
    float horizontal_move = 0f;
    public float jump_hight = 3;
    private bool m_facing_right = true; 
	private Vector3 m_velocity = Vector3.zero;
    [Range(0, 1f)] [SerializeField] private float m_movement_smoothing = .05f;	// How much to smooth out the movement

    public Animator m_animator;
    private Rigidbody2D m_rigitbody;
    [SerializeField] private Transform m_ground_check;
    [SerializeField] private LayerMask ground_layer;
    //public UnityEvent OnLandEvent;
    [SerializeField] private bool is_groundet = false;
    const float k_GroundedRadius = .2f;
    


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

        if (move < 0 && m_facing_right){
            Flip();
        }
        else if (move > 0 && !m_facing_right){
            Flip();
        }
    }

    private void Ground_check(){
        is_groundet = false;
        Collider2D[] collider = Physics2D.OverlapCircleAll(m_ground_check.position, k_GroundedRadius, ground_layer);
        if(collider.Length > 0){
            is_groundet = true;
        }
    }

//Start and Update
//===================================================================================================================================

    private void Start(){
        m_rigitbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    private void Update(){
        //Debug.Log(Time.deltaTime);
        //Debug.Log(horizontal_move);

        horizontal_move = Input.GetAxis("Horizontal") * movement_speed;
        m_animator.SetFloat("Speed", Mathf.Abs(horizontal_move));
        m_animator.SetBool("is_grounded", is_groundet);       
    }


    private void FixedUpdate() {
        Ground_check();
        Move(horizontal_move * Time.fixedDeltaTime);
        Debug.Log(is_groundet);
        //Debug.Log(horizontal_move);
        //Debug.Log(Time.fixedDeltaTime);
        //Debug.Log(m_ridgitbody.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(m_rigitbody.velocity.y) < 0.001f) 
        {
            m_rigitbody.AddForce(new Vector2(0, jump_hight), ForceMode2D.Impulse);
        }
    }
}