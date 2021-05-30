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
    [SerializeField] private bool playerInTrigger = false;


    /** Dialogue to be triggered. */
    public virtual Dialogue GetActiveDialogue() => defaultDialogue;

    public void Trigger() {
        DialogueManager.Log("Triggering dialogue");
        DialogueManager.Instance.lastKeyPress = Time.fixedTime;
        DialogueManager.Instance.StartDialogue(GetActiveDialogue());
    }


    private void Update() {
        if (!playerInTrigger) {
            return;
        }

        if(DialogueManager.Instance.IsDialogueActive()) {
            DialogueManager.Log("Won't trigger dialogue; another one is still active");
            return;
        }

        if (DialogueManager.Instance.instantTrigger) {
            DialogueManager.Log("Triggering instant dialogue");
            DialogueManager.Instance.instantTrigger = false;
            Trigger();
            return;
        }

        float keypressDelta = Time.fixedTime - DialogueManager.Instance.lastKeyPress;
        if (keypressDelta < DialogueManager.KEY_PRESS_TIME_DELTA) {
            DialogueManager.Log("Won't trigger dialogue; too fast (" + keypressDelta + " < min delta)");
            return;
        }

        if (Input.GetKeyDown(DialogueManager.DIALOGUE_KEY)){
            DialogueManager.Log("Triggering dialogue from keypress");
            Trigger();
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInTrigger = true;
            if (!DialogueManager.Instance.IsDialogueActive()) {
                DialogueManager.Instance.HintAt(GetActiveDialogue());
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerInTrigger = false;
            DialogueManager.Instance.ClearHint();
        }
    }
}
