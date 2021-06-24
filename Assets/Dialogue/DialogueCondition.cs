using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueCondition {

    public abstract bool IsFulfilled();

    public static bool ConditionsFullFilled(List<DialogueCondition> conditions) {
        foreach (DialogueCondition c in conditions)
            if (!c.IsFulfilled()) return false;
        return true;
    }
}

public class HasItem : DialogueCondition {
    private Item requiredItem;
    public HasItem(Item item) {
        requiredItem = item;
    }
    public override bool IsFulfilled() {
        return Inventory.Instance.HasItem(requiredItem);
    }
}

public class DoesNotHaveItem : DialogueCondition {
    private Item requiredNotItem;
    public DoesNotHaveItem(Item item) {
        requiredNotItem = item;
    }
    public override bool IsFulfilled() {
        return !Inventory.Instance.HasItem(requiredNotItem);
    }
}