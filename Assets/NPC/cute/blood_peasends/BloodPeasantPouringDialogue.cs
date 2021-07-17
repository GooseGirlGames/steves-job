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
    public static BloodPeasantPouringDialogue t;
    private bool isPouring = false;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(PeaseantSoakedCatHappy) || isPouring) {
            return null;
        }

        return new PourDia();
    }

    public const float FLOOR_Y = 7.95f;

    private void Awake() {
        t = this;
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
        StartCoroutine(PourAnimation(cute));
    }

    
    private IEnumerator PourAnimation(bool cute) {
        isPouring = true;
        TargetCamera.Disable();
        stevecontroller player = GameObject.FindObjectOfType<stevecontroller>();
        Vector3 playerPos = player.gameObject.transform.position;
        
        player.Lock("BloodPouring", hide: false);
        TargetCamera.Target(Peasants.gameObject.transform, blendTime: 5.0f);
        yield return new WaitForSeconds(3.5f);
        player.Lock("BloodPouring", hide: true);
        renderer.enabled = true;
        yield return new WaitForSeconds(1.5f);
        TargetCamera.Disable();
        yield return new WaitForSeconds(2.6f);
        yield return new WaitForSeconds(1);
        PourAnimator.SetTrigger("StartPouring");
        if (cute) {
            Inventory.Instance.AddItem(EmptyBucketCute);
        } else {
            Inventory.Instance.AddItem(EmptyBucket);
        }
        yield return new WaitForSeconds(1);
        transitionAnimation.SetTrigger("ExitScene");
        yield return new WaitForSeconds(1);
        renderer.enabled = false;
        yield return new WaitForSeconds(2);
        Inventory.Instance.AddItem(PeaseantSoakedCatHappy);
        Peasants.GetComponent<PeasentsVisible>().UpdateVisibility();
        BloodPeasents.GetComponent<SoakedPeasantsVisible>().UpdateVisibility();
        player.Unlock("BloodPouring");
        transitionAnimation.SetTrigger("EnterScene");
        isPouring = false;
    }
}

public class PourDia : Dialogue {
    public PourDia() {
        BloodPeasantPouringDialogue trigger = BloodPeasantPouringDialogue.t;

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
            new OtherItemOption().IfChosen(new TriggerDialogueAction<WontPourThis>())
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
            new OtherItemOption().IfChosen(new TriggerDialogueAction<WontPourThis>())
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

public class WontPourThis : Dialogue {
    public WontPourThis() {
        Say("Nope, not gonna throw this down here!")
        .DoAfter(new TriggerDialogueAction<PourDia>());
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
