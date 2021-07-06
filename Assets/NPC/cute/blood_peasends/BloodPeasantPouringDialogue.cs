using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantPouringDialogue : DialogueTrigger {
    public Item BloodBucket;
    public Item EmptyBucket;
    public Item PeaseantSoakedCatHappy;
    public Animator PourAnimator;
    public Animator transitionAnimation;

    public GameObject Peasants;
    public GameObject BloodPeasents;
    // public Animator PeasantAnimator;  TODO even Needed?
    new private SpriteRenderer renderer;
        public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(PeaseantSoakedCatHappy)) {
            return null;
        }
        return new PourDia(this);
    }

    public const float FLOOR_Y = 7.95f;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = Inventory.Instance.HasItem(PeaseantSoakedCatHappy);
        if (transitionAnimation) {
            transitionAnimation.SetFloat("Speed", 0.6f);
        }
    }
    public void Pour() {
        if (Inventory.Instance.HasItem(PeaseantSoakedCatHappy)) {
            return;
        }
        Inventory.Instance.AddItem(PeaseantSoakedCatHappy);
        Inventory.Instance.AddItem(EmptyBucket);
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
        yield return new WaitForSeconds(2.6f);
        PourAnimator.SetTrigger("StartPouring");
        yield return new WaitForSeconds(2);
        transitionAnimation.SetTrigger("ExitScene");
        yield return new WaitForSeconds(1);
        Peasants.GetComponent<Renderer>().enabled = false;
        BloodPeasents.GetComponent<Renderer>().enabled = true;
        renderer.enabled = false;
        yield return new WaitForSeconds(2);
        player.Unlock("BloodPouring");
        transitionAnimation.SetTrigger("EnterScene");

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