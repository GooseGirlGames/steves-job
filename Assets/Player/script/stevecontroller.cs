using UnityEngine;
using System;

public class stevecontroller : MonoBehaviour {

    public float MovementSpeed = 1;
    private void Start(){
    }

    private void Update(){
        var movement = Input.GetAxis("Horizontal");
        Console.WriteLine("movement");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;       
    }
}