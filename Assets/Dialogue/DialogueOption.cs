using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class DialogueOption
{
    public enum DialogueOptionType {
        /* Action relating to an item.  An icon of that item will be displayed.
         * The option will only be available if the player has that item.
         */
        ItemAction,
        /* Action independent of any item. */
        TextAction
    }
    public DialogueOptionType dialogueOptionType = DialogueOptionType.ItemAction;
    [Tooltip("For ItemActions this typically is a verb.")]
    public string text;
    public Item item;
    public List<UnityEvent> onChose;
}
