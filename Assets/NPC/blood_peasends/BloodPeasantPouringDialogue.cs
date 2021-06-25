using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantPouringDialogue : DialogueTrigger {
    public Item BloodBucket;
    public Item EmptyBucket;
    public Animator PourAnimator;
    public Animator PeasantAnimator;
    new private SpriteRenderer renderer;
    public override Dialogue GetActiveDialogue() {
        return new PourDia(this);
    }

    public const float FLOOR_Y = 7.05f;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    public void Pour() {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.gameObject.transform.position;
        //this.gameObject.transform.position = new Vector3(
        //    player.x
        //);
        
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
        );
    }
}

public class EmptyBucketDia : Dialogue {
    public EmptyBucketDia() {
        Say("There's nothing in this bucket...");
    }
}