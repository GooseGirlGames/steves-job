using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroVoidCamera : MonoBehaviour
{
    private static bool triggered = false;
    /**
     * Set the camera's priority to -10 after a delay.
     */
    IEnumerator LowPriorityWithDelay(float seconds) {
        IntroVoidCamera.triggered = true;
        yield return new WaitForSeconds(seconds);
        SetPriority(-10);
    }

    void Awake() {
        if (!triggered) {
            SetPriority(100);
            StartCoroutine(LowPriorityWithDelay(2.0f));
        }
    }

    void SetPriority(int priority) {
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = priority;
    }
}
