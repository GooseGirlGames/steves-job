using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetCamera : MonoBehaviour
{
    private static TargetCamera Instance;
    private CinemachineVirtualCamera cam;
    public static float IN_TRANSITION_ANIMATION_DURATION = 2f;  // seconds
    public static float OUT_TRANSITION_ANIMATION_DURATION = 0.5f;  // seconds

    public static void Target(Transform target) {
        Instance.SetTarget(target);
        Instance.SetEnabled(true);
    }

    public static void Disable() {
        Instance.SetEnabled(false);
    }

    private void SetTarget(Transform target) {
        cam.Follow = target;
    }
    private void SetEnabled(bool enable) {
        cam.Priority = enable ? 15 : 5;
    }

    void Awake() {
        TargetCamera.Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    
}
