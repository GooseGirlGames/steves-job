using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroVoidCamera : MonoBehaviour {
    public Item _switch_not_picked_up;
    /**
     * Set the camera's priority to -10 after a delay.
     */
    IEnumerator LowPriorityWithDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        SetPriority(-10);
    }

    void Awake() {
        if (Inventory.Instance.HasItem(_switch_not_picked_up)) {
            SetPriority(100);
            StartCoroutine(LowPriorityWithDelay(2.0f));
        }
    }

    void SetPriority(int priority) {
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = priority;
    }
}
