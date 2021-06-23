using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    void Awake() {
        GameObject f_cam = GameObject.Find("CameraFixed");
        CinemachineVirtualCamera v_cam = GetComponent<CinemachineVirtualCamera>();
        v_cam.Follow = f_cam.transform;
    }
}
