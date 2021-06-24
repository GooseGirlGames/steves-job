using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OldSentence
{
    [SerializeField]
    public string name;
    public string text;
    public Sprite avatar;
    [Tooltip("Events triggered when the sentence after this one is triggered or the dialogue ends.")]
    public List<UnityEvent> onComplete;
    public List<DialogueOption> options;
    [Tooltip("Leave empty to use the default background.  Overrides background set in Dialogue if set.")]
    public Sprite background;
}
