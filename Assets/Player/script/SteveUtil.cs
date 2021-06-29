using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveUtil {
    public static bool IsSteve(Collider2D collider) {
        return collider.gameObject.CompareTag("Player");
    }
}