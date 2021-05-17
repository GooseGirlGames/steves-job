using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;
    private bool playerInTrigger = false;
    public void Trigger() {
        DialogueManager.Instance.StartDialogue(dialogue);
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
