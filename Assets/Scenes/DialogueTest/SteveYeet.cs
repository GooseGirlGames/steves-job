using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveYeet : MonoBehaviour
{
    public void Yeet() {
        Debug.Log("Yeet the Steve!");
        GameObject steve = GameObject.FindGameObjectWithTag("Player");
        steve.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 15, 0);
    }
}
