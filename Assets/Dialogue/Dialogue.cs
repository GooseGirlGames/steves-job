using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public List<Sentence> sentences;
    [Tooltip("Position of the dialogue box's bottom center point within the scene.")]
    public Transform diaboxPosition;
    public Sprite background;

}
