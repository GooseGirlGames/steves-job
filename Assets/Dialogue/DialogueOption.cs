using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class DialogueOption
{
    public enum DialogueOptionType {
        ItemAction,
    }
    public DialogueOptionType dialogueOptionType = DialogueOptionType.ItemAction;
    public string verb;
    public Item item;
    public List<UnityEvent> onChose;
}