using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueElement {
    Queue<DialogueElement> children = new Queue<DialogueElement>();
    public DialogueElement current = null;
    private List<Condition> conditions = new List<Condition>();
    protected bool m_isBase = true;  // XXX i hate this

    public bool HasNext() {
        return children.Count > 0;
    }
    public DialogueElement Next() {

        /*if (current is LazyDialogue) {
            Debug.Log("Unpacking lazy dialogue?");
            ((LazyDialogue) current).Unpack();
        }*/

        DialogueElement next = children.Peek();
        if (next is LazyDialogue) {
            Debug.Log("Unpacking lazy dialogue");
            ((LazyDialogue)next).Unpack();
        }

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
        conditions.Add(condition);
        return this;
    }

    public bool ConditionsMet() {
        foreach (Condition condition in conditions) {
            if (!condition.IsMet()) return false;
        }
        return true;
    }

    public int Count() {
        return children.Count;
    }

    public bool IsBase() {
        return m_isBase;
    }
}
