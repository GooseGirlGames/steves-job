using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueElement {
    Queue<DialogueElement> children = new Queue<DialogueElement>();
    private List<Condition> conditions = new List<Condition>();

    public bool HasNext() {
        return children.Count > 0;
    }
    public DialogueElement Next() {
        return children.Dequeue();
    }
    public DialogueElement Append(DialogueElement element) {
        children.Enqueue(element);
        return this;
    }
    public DialogueElement Condition(Condition condition) {
        conditions.Add(condition);
        return this;
    }

    public bool ConditionsMet() {
        foreach (Condition condition in conditions) {
            if (!condition.IsMet()) return false;
        }
        return true;
    }
}
