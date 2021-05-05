using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    [SerializeField]
    public bool AllowOnlyOneInstance = false;
    public static GameObject Instance;

    void Awake() {
        if (AllowOnlyOneInstance) {
            if (Instance != null) {
                GameObject.Destroy(this.gameObject);
            } else {
                Instance = this.gameObject;
            }
        }
        DontDestroyOnLoad(this);
    }
}
