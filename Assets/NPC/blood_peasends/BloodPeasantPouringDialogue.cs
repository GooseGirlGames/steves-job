using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantPouringDialogue : DialogueTrigger {
    public Item BloodBucket;
    public Item EmptyBucket;
    public Animator PourAnimator;
    public GameObject Peasants;
    // public Animator PeasantAnimator;  TODO even Needed?
    new private SpriteRenderer renderer;
    public override Dialogue GetActiveDialogue() {
        return new PourDia(this);
    }

    public const float FLOOR_Y = 7.95f;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    public void Pour() {
        StartCoroutine(PourAnimation());
    }
    private IEnumerator PourAnimation() {
        TargetCamera.Disable();
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        Vector3 playerPos = player.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(
            playerPos.x,
            FLOOR_Y,
            0
        );
        renderer.enabled = true;
        player.Lock("BloodPouring", hide: true);
        TargetCamera.Target(Peasants.gameObject.transform, blendTime: 5.0f);
        yield return new WaitForSeconds(5);
        TargetCamera.Disable();
        yield return new WaitForSeconds(2);
        PourAnimator.SetTrigger("StartPouring");
    }
}

public class PourDia : Dialogue {
    public PourDia(BloodPeasantPouringDialogue trigger) {

        Say("...")
        .Choice(
            new ItemOption(trigger.BloodBucket).IfChosen(new DialogueAction(trigger.Pour))
        )
        .Choice(
            new ItemOption(trigger.EmptyBucket).IfChosen(new TriggerDialogueAction<EmptyBucketDia>())
        )
        .Choice(
            new TextOption("Do nothing")
        )
        .Do(() => {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa");
            TargetCamera.Disable();
        });
    }
}

public class EmptyBucketDia : Dialogue {
    public EmptyBucketDia() {
        Say("There's nothing in this bucket...");
    }
}