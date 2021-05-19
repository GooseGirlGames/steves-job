using UnityEngine;
using UnityEngine.Events;
using System;

public class menucontroller : MonoBehaviour {

//member variablen
//===================================================================================================================================
    
    float horizontal_move = 20f;
    private bool m_facing_right = true; 
    public bool active= false;
    private Vector3 m_velocity = Vector3.zero;
    [Range(0, 1f)] [SerializeField] private float m_movement_smoothing = .05f;    // How much to smooth out the movement

    public Animator m_animator;
    private Rigidbody2D m_rigitbody;


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

    public void set_active(){
        active = true;
    }

//Start and Update
//===================================================================================================================================

    private void Start(){
        m_rigitbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    private void Update(){
        m_animator.SetFloat("Speed", Mathf.Abs(horizontal_move));
    }


    private void FixedUpdate() {
        if(active){
            Move(horizontal_move * Time.fixedDeltaTime);
        }
    }
}
