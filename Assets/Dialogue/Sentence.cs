using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class Sentence
{
    [SerializeField]
    public string name;
    public string text;
    [Tooltip("Events triggered when the sentence after this one is triggered or the dialogue ends.")]
    public List<UnityEvent> onComplete;
    public List<DialogueOption> options;

}
