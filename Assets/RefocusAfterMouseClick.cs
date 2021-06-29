using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://answers.unity.com/questions/943854/ui-46-disable-mouse-from-s$$anonymous$$ling-focus.html#
public class RefocusAfterMouseClick : MonoBehaviour {
    GameObject lastSelected = null;

    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null) {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        } else {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
}