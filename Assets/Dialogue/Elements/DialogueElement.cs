using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueElement {
    Queue<DialogueElement> children = new Queue<DialogueElement>();
    private DialogueElement current = null;
    private List<Condition> conditions = new List<Condition>();

    public bool HasNext() {
        return children.Count > 0;
    }
    public DialogueElement Next() {
        if ((current != null) && current.HasNext()) {
            return current.Next();
        }
        current = children.Dequeue();
        return current;
    }
    public DialogueElement Append(DialogueElement element) {
        children.Enqueue(element);
        return this;
    }
    public DialogueElement Condition(Condition condition) {
        Debug.Log("Adding condition");
        conditions.Add(condition);
        return this;
    }

    public bool ConditionsMet() {
        Debug.Log(conditions.Count);
        foreach (Condition condition in conditions) {
            if (!condition.IsMet()) return false;
        }
        return true;
    }
}
