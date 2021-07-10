using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    public bool enable = true;
    void Awake() {
        if (!enable) return;
        GameObject f_cam = GameObject.Find("CameraFixed");
        CinemachineVirtualCamera v_cam = GetComponent<CinemachineVirtualCamera>();
        v_cam.Follow = f_cam.transform;
    }
}
