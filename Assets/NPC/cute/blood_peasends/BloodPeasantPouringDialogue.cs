using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantPouringDialogue : DialogueTrigger {
    public Item BloodBucket;
    public Item EmptyBucket;
    public Item BloodBucketCute;
    public Item EmptyBucketCute;
    public Item PeaseantSoakedCatHappy;
    public Item Cranked;
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
    public void CutePour() {
        Pour(cute: true);
    }
    public void HorrorPour() {
        Pour(cute: false);
    }
    public void Pour(bool cute) {
        if (Inventory.Instance.HasItem(PeaseantSoakedCatHappy)) {
            return;
        }
        Inventory.Instance.AddItem(PeaseantSoakedCatHappy);
        if (cute) {
            Inventory.Instance.AddItem(EmptyBucketCute);
        } else {
            Inventory.Instance.AddItem(EmptyBucket);
        }
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
        Peasants.GetComponent<PeasentsVisible>().UpdateVisibility();
        BloodPeasents.GetComponent<SoakedPeasantsVisible>().UpdateVisibility();
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
            new ItemOption(trigger.BloodBucket).IfChosen(new DialogueAction(trigger.HorrorPour))
        )
        .Choice(
            new ItemOption(trigger.EmptyBucket).IfChosen(new TriggerDialogueAction<EmptyBucketDia>())
        )
        .Choice(
            new ItemOption(trigger.BloodBucketCute).IfChosen(new DialogueAction(trigger.CutePour))
        )
        .Choice(
            new ItemOption(trigger.EmptyBucketCute).IfChosen(new TriggerDialogueAction<EmptyBucketDia>())
        )
        .Choice(
            new TextOption("Do nothing")
        )
        .Do(() => {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa");
            TargetCamera.Disable();
        })
        .If(HasItem(trigger.Cranked));


        Say("...")
        .Choice(
            new ItemOption(trigger.BloodBucket)
            .IfChosen(new TriggerDialogueAction<MarquueDia>())
        )
        .Choice(
            new ItemOption(trigger.EmptyBucket)
            .IfChosen(new TriggerDialogueAction<EmptyBucketDia>())
        )

        .Choice(
            new ItemOption(trigger.BloodBucketCute)
            .IfChosen(new TriggerDialogueAction<MarquueDia>())
        )
        .Choice(
            new ItemOption(trigger.EmptyBucketCute)
            .IfChosen(new TriggerDialogueAction<EmptyBucketDia>())
        )

        .Choice(
            new TextOption("Do nothing")
        )
        .Do(() => {
            TargetCamera.Disable();
        })
        .If(DoesNotHaveItem(trigger.Cranked));


    }
}

public class EmptyBucketDia : Dialogue {
    public EmptyBucketDia() {
        Say("There's nothing in this bucket...");
    }
}


public class MarquueDia : Dialogue {
    public MarquueDia() {
        Say("If only that marquee wasn't in the way...");
    }
}