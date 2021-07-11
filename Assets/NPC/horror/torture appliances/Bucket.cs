using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : DialogueTrigger
{
    public Item bucket;

    public Item _given_horrorcoin;
    public Item _got_bucket;

    public const string DefName = "Steve E Horror";
    public const string UwuName = "Steve E Howwow";

    public static Bucket b;

    void Awake() {
       Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
       Bucket.b = this;

        if (! Inventory.Instance.HasItem(_got_bucket)) {
            if (Inventory.Instance.HasItem(_given_horrorcoin)) {
                name = DefName;
                return new BucketGet();
            }
            name = UwuName;
            return new BucketHandsOff();
       }
       name = DefName;
       return new BucketInInv();
    }

    public class BucketHandsOff : Dialogue {
        public BucketHandsOff() {
            Say(Uwu.Uwufy("Excuse me?"));
            Say(Uwu.Uwufy("Get away from my stuff!"));
        }
    }

    public class BucketGet : Dialogue {
        public BucketGet() {
            Bucket b = Bucket.b;

            Say("Yeah, thats an old bucket i have no use for anymore");
            Say("Sure, you could use it for waterboarding or maybe even chinese water torture");
            Say("But I prefer to sell full instrument sets and not just single components");
            Say("If you want him just take him");
            Say("I'd be happy to get rid of it")
            .DoAfter(GiveItem(b.bucket))
            .DoAfter(GiveItem(b._got_bucket))
            .DoAfter(new DialogueAction(() => {
            GameObject.Find("Bucket").SetActive(false); }));
                
        }
    }

    public class BucketInInv : Dialogue {
        public BucketInInv() {
            Say("Now that there is free space I can get another torture instrument");
        }
    }

}
