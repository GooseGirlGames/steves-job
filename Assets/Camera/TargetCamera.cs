using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetCamera : MonoBehaviour
{
    private static TargetCamera Instance;
    private CinemachineVirtualCamera cam;
    public CinemachineBrain brain;
    public CinemachineBlendDefinition overrideBlend;
    private static float? blendTimeOverride = null;
    private static bool hasMovedIn = false;
    public static float IN_TRANSITION_ANIMATION_DURATION = 2f;  // seconds
    public static float OUT_TRANSITION_ANIMATION_DURATION = 0.5f;  // seconds

    public static void Target(Transform target, float? blendTime = null) {
        TargetCamera.blendTimeOverride = blendTime;
        Instance.SetTarget(target);
        Instance.SetEnabled(true);
        hasMovedIn = false;
    }

    public static void Disable() {
        Instance.SetEnabled(false);
        // blendTimeOverride is reset in GetBlendOverrideDelegate()
    }

    private void SetTarget(Transform target) {
        cam.Follow = target;
    }
    private void SetEnabled(bool enable) {
        cam.Priority = enable ? 15 : 5;
    }

    void Awake() {
        TargetCamera.Instance = this;
        CinemachineCore.GetBlendOverride = GetBlendOverrideDelegate;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public static CinemachineBlendDefinition GetBlendOverrideDelegate(
            ICinemachineCamera from,
            ICinemachineCamera to,
            CinemachineBlendDefinition defaultBlend,
            MonoBehaviour owner) {

        if (TargetCamera.blendTimeOverride is float blendTime) {
            TargetCamera.Instance.overrideBlend.m_Time = blendTime;
            if (TargetCamera.hasMovedIn) {
                TargetCamera.blendTimeOverride = null;
                TargetCamera.hasMovedIn = false;
            }
            hasMovedIn = true;
            return TargetCamera.Instance.overrideBlend;
        }
        return defaultBlend;
  }
    
}
