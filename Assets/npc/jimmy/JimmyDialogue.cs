using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : MonoBehaviour
{
    public Portal portalToMiniGame;
    public Item bucket;
    public Item empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided!");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("with player");
            Trigger();
        }
    }

    public void EnterMiniGame() {
        portalToMiniGame.TriggerTeleport();
    }

    [SerializeField]
    public Dialogue dialogue;
    public Dialogue loserdia;
    public void Trigger() {
        if (Inventory.Instance.HasItem(bucket)) {
            return;
        }
        else if (Inventory.Instance.HasItem(empty)){
            DialogueManager.Instance.StartDialogue(loserdia);
        }
        else DialogueManager.Instance.StartDialogue(dialogue);
    }
}
