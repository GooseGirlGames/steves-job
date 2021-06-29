using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantCameraHint : MonoBehaviour {
    public GameObject Peasants;
    private void OnTriggerEnter2D(Collider2D other) {
        if (!SteveUtil.IsSteve(other)) return;
        StartCoroutine(DelayHintingPeasants(1.5f));
    }

    private IEnumerator DelayHintingPeasants(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        TargetCamera.Target(Peasants.transform);
        yield return new WaitForSeconds(TargetCamera.IN_TRANSITION_ANIMATION_DURATION);
        TargetCamera.Disable();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!SteveUtil.IsSteve(other)) return;
        TargetCamera.Disable();
    }
}
