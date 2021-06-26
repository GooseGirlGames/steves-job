using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CoolButton : Button, ISelectHandler
/* This is garbage and I hate it */
{
    public bool Selected = false;
     public override void OnSelect(BaseEventData eventData) {
        Selected = true;         
        base.OnSelect(eventData);
     }

    public override void OnDeselect(BaseEventData eventData) {
        Selected = false;
        base.OnDeselect(eventData);
    }
}
/* Soggy */