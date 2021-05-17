using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredDialogue : MonoBehaviour
{

    public Item shirt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Trigger();
        }
    }

    public void GiveShirt() {
        Inventory.Instance.AddItem(shirt);
    }

    [SerializeField]
    public Dialogue dialogue;
    public void Trigger() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
