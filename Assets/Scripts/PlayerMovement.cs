using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime,false,false);
    }

    void LateUpdate()
    {

    }
}
