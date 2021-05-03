using UnityEngine;
using UnityEngine.Events;
using System;

public class stevecontroller : MonoBehaviour {

    //member variable

    public float MovementSpeed = 5;
    public float Jumphight = 3;
    public Animator animator;

    //float horizontalMove = 0f;

    private Rigidbody2D _ridgitbody;

	private void Flip(){
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

//===================================================================================================================================

    private void Start()
    {
        _ridgitbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;       

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_ridgitbody.velocity.y) < 0.001f)
        {
            _ridgitbody.AddForce(new Vector2(0, Jumphight), ForceMode2D.Impulse);
        }
    }
}