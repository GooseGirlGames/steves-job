using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public abstract class DialogueOption {
    public List<DialogueAction> onChose = new List<DialogueAction>();
    public List<DialogueCondition> conditions = new List<DialogueCondition>();
    public DialogueOption IfChosen(DialogueAction action) {
        onChose.Add(action);
        return this;
    }
    public DialogueOption AddCondition(DialogueCondition condition) {
        conditions.Add(condition);
        return this;
    }
}

public class TextOption : DialogueOption {
    public String text;
    public TextOption(String text) {
        this.text = text;
    }
}

public class ItemOption : DialogueOption {
    public Item item;
    public ItemOption(Item item) {
        this.item = item;
        AddCondition(new HasItem(item));
    }
}

public class OtherItemOption : DialogueOption {
    
}