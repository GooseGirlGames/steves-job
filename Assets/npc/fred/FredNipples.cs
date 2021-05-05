using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredNipples : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator nipple_animator;
    public Item shirt;
    [SerializeField] bool shirt_on = true;
    void Start()
    {
        nipple_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.Instance.HasItem(shirt)){
        shirt_on = !Inventory.Instance.has_shirt;
        }

        nipple_animator.SetBool("shirt", shirt_on);
    }
}
