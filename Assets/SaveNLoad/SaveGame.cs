using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGame {
    public List<int> inventory = new List<int>();
    public string scene;
    public float pos_x = 0;
    public float pos_y = 0;
    public float pos_z = 0;
}
