using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Apply this or a derived behavior to an NPC that can be talked to.
 *
 * The NPC must have a RigidBody2D component with 'Is Trigger' enabled.
 * If the player stands within that RigidBody2D and presses the 'E' button,
 * the dialogue is triggered.
 *
 * Unless the NPC just says the exact same Dialogue every time, extend this
 * class and add logic to decide which Dialogue to trigger by overriding the
 * `GetGetActiveDialogue` method.  For a simple example of such a behavior, see
 * `Assets/Scenes/DialogueTest/SampleExtendedDialogueTrigger.cs`.
 */
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue defaultDialogue;
    private bool playerInTrigger = false;


    /** Dialogue to be triggered. */
    public virtual Dialogue GetActiveDialogue() => defaultDialogue;

    public void Trigger() {
        DialogueManager.Instance.StartDialogue(GetActiveDialogue());
    }


    private void Update() {
        if (playerInTrigger && Input.GetKeyDown(DialogueManager.DIALOGUE_KEY)){
            if(!DialogueManager.Instance.IsDialogueActive()) {
                Debug.Log("yes");
                Trigger();
            }
            
        }
        if (DialogueManager.Instance.instantTrigger && playerInTrigger){
            if(!DialogueManager.Instance.IsDialogueActive()) {
                Trigger();
                DialogueManager.Instance.instantTrigger = false;
            }
                //Debug.Log("Trigger");
                
        }
        //Debug.Log(DialogueManager.Instance.instantTrigger + " instant trigger");
        //Debug.Log(playerInTrigger + " in trigger");
        //Debug.Log(Input.GetKeyDown(DialogueManager.DIALOGUE_KEY) + " key");

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
