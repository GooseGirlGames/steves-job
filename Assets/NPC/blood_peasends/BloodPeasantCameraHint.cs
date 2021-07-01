using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPeasantCameraHint : MonoBehaviour {
    public GameObject Peasants;
    [SerializeField] private Item soaked;
    private Coroutine hintCoroutine = null;
    private void OnTriggerEnter2D(Collider2D other) {
        if (!SteveUtil.IsSteve(other)) return;
        if (Inventory.Instance.HasItem(soaked)) return;
        
        if (Inventory.Instance.HasItemByName("JimmyTheCatFinished")) return;
        if (hintCoroutine == null) {
            hintCoroutine = StartCoroutine(DelayHintingPeasants(1.5f));
        }
    }

    private IEnumerator DelayHintingPeasants(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        TargetCamera.Target(Peasants.transform);
        yield return new WaitForSeconds(TargetCamera.IN_TRANSITION_ANIMATION_DURATION);
        TargetCamera.Disable();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!SteveUtil.IsSteve(other)) return;
        if (Inventory.Instance.HasItem(soaked)) return;
        
        if (hintCoroutine != null) {
            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
        }
        TargetCamera.Disable();
    }
}
