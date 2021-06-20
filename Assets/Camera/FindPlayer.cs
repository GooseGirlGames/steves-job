using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    void Awake() {
        GameObject player = GameObject.Find("Player");
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Follow = player.transform;
    }
}
