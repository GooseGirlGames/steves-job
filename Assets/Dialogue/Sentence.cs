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
    public List<UnityEvent> onComplete;
    public List<DialogueOption> options;

}
