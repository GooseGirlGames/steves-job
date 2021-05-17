using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredNipples : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator nipple_animator;
    public Item shirt;
    [SerializeField] bool shirt_on = true;
    

    // Update is called once per frame
    void Update()
    {
        shirt_on = !Inventory.Instance.HasItem(shirt);
     

        nipple_animator.SetBool("shirt", shirt_on);
    }
}
