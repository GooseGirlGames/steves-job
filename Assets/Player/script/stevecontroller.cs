using UnityEngine;
using System;

public class stevecontroller : MonoBehaviour {

    public float MovementSpeed = 5;
    public float Jumphight = 3;

    private Rigidbody2D _ridgitbody;

    private void Start()
    {
        _ridgitbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        Console.WriteLine("movement");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;       

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_ridgitbody.velocity.y) < 0.001f)
        {
            _ridgitbody.AddForce(new Vector2(0, Jumphight), ForceMode2D.Impulse);
        }
    }
}