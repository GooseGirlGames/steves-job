using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VoidNPCDiaTrigger : DialogueTrigger {
    public Item _restored_cute;
    public Item _restored_horror;
    private Animator animator;
    private VoidNPCState state;

    private void Awake() {
        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogWarning("Void NPC " + name + " needs an animator!");
        }
        UpdateStateAndAnimator();
    }

    /**
    Implementation should look somewhat like this:

    private static HandymanDialogue t;
    public override void UpdateStaticT() {
        t = this;
    }
    */
    public abstract void UpdateStaticT();

    public override Dialogue GetActiveDialogue() {
        UpdateStaticT();
        UpdateStateAndAnimator();

        if (state == VoidNPCState.Full) {
            return NewFullRestoredDia();
        } else if (state == VoidNPCState.Half) {
            return NewHalfRestoredDia();
        }
        return NewGoneDia();
    }

    public abstract Dialogue NewFullRestoredDia();
    public abstract Dialogue NewHalfRestoredDia();
    public abstract Dialogue NewGoneDia();


    public void UpdateStateAndAnimator() {
        if (Inventory.Instance.HasItem(_restored_cute)
                && Inventory.Instance.HasItem(_restored_horror)) {
            state = VoidNPCState.Full;
        } else if (Inventory.Instance.HasItem(_restored_cute)
                    || Inventory.Instance.HasItem(_restored_horror)) {
            state = VoidNPCState.Half;
        } else {
            state = VoidNPCState.Gone;
        }

        if (animator != null) {
            animator.SetInteger("State", (int) state);
        }
        
    }
}