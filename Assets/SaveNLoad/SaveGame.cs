using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGame {
    public List<int> inventory = new List<int>();
    public string scene;
    public float pos_x;
    public float pos_y;
    public float pos_z;
}
