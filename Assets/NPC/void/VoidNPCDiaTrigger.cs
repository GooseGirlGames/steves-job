using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VoidNPCDiaTrigger : DialogueTrigger {
    public Item _restored_cute;
    public Item _restored_horror;
    private Animator animator;
    public List<Animator> additionalAnimators = new List<Animator>();
    new private SpriteRenderer renderer;
    private VoidNPCState state;
    public Sprite avatar_normal;
    public Sprite avatar_half;
    public Sprite avatar_gone;
    private Color white = new Color(1, 1, 1, 1);
    private Color gray = new Color(0, 0, 0, 0.6f);

    private void Awake() {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
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
            avatar = avatar_normal;
        } else if (Inventory.Instance.HasItem(_restored_cute)
                    || Inventory.Instance.HasItem(_restored_horror)) {
            state = VoidNPCState.Half;
            avatar = avatar_half;
        } else {
            state = VoidNPCState.Gone;
            avatar = avatar_gone;
        }

        foreach (var a in additionalAnimators) {
            a.SetInteger("State", (int) state);
        }
        if (animator != null) {
            animator.SetInteger("State", (int) state);
            renderer.color = (state == VoidNPCState.Gone) ? gray : white;
        }
        
    }
}