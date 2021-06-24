
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using TMPro;


public class LazyDialogue : DialogueElement
{
    public delegate DialogueElement GetDialogue();
    public GetDialogue Get;
    public LazyDialogue(GetDialogue get) {
        Get = get;
        m_isBase = false;
    }

    public void Unpack() {
        if (!HasNext()) {
            Append(Get());
            Debug.Log("Unpacked " + Count() + " elems.");
        } else {
            Debug.Log("Not unpacking");
        }
    }
}
