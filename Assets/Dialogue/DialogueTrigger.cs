using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue defaultDialogue;
    private bool playerInTrigger = false;

    public virtual Dialogue GetActiveDialogue() => defaultDialogue;
    
    public void Trigger() {
        DialogueManager.Instance.StartDialogue(GetActiveDialogue());
    }

    private void Update() {
        if (playerInTrigger && Input.GetKeyDown(DialogueManager.DIALOGUE_KEY)) {
            if(!DialogueManager.Instance.IsDialogueActive()) {
                Trigger();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
        }
    }
}
