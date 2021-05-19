using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorcollider : MonoBehaviour{
    public Animator dooranimator;
    private void OnTriggerEnter2D(Collider2D other) {
        dooranimator.SetTrigger("open");
    }
}
