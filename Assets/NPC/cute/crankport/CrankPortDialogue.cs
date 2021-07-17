using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankPortDialogue : DialogueTrigger {
    public static CrankPortDialogue t;

    public Item candyCrank;
    public Item cranked;
    public Animator animator;
    public Sprite ava_cranked;
    public Sprite ava_uncranked;
    public GameObject marqeePhysics;
    private void Awake() {
        if (Inventory.Instance.HasItem(cranked)) {
            avatar = ava_cranked;
            animator.SetTrigger("RetractInstant");
        } else {
            avatar = ava_uncranked;
        }
    }

    public override Dialogue GetActiveDialogue() {
        CrankPortDialogue.t = this;
        //if (!Inventory.Instance.HasItem(cranked))
            return new CrankPort();
        //return null;
    }

    private void CrankMarquee() {
        if (Inventory.Instance.HasItem(cranked)) {
            Inventory.Instance.RemoveItem(cranked);
            animator.SetTrigger("Extend");
            avatar = ava_uncranked;
            marqeePhysics.SetActive(true);
        } else {
            Inventory.Instance.AddItem(cranked);
            animator.SetTrigger("Retract");
            avatar = ava_cranked;
            marqeePhysics.SetActive(false);
        }
    }

    public class CrankPort : Dialogue {
        public CrankPort() {
            Say("...")
            .Choice(new TextOption("..."))
            .Choice(
                new ItemOption(t.candyCrank)
                .IfChosen(new TriggerDialogueAction<Crank>())
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<CantCrank>())
            );
        }
    }

    public class Crank : Dialogue {
        public Crank() {
            CrankPortDialogue t = CrankPortDialogue.t;
            EmptySentence().DoAfter(t.CrankMarquee);
            Say("*squeak*");
        }
    }

    public class CantCrank : Dialogue {
        public CantCrank() {
            Say("A " + DialogueManager.Instance.currentItem.name + " won't fit in here.")
            .DoAfter(new TriggerDialogueAction<CrankPort>());
        }
    }
}
