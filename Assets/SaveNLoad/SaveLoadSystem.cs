using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem {
    public const string DEFAULT_SAVE_FILE = "steve";
    private static Vector3 playerPos;
    public static void DeleteSave(string relpath = DEFAULT_SAVE_FILE) {
        string path = Application.persistentDataPath + "/" + relpath;
        if (File.Exists(path))
            File.Delete(path);
    }
    private static SaveGame CreateSave() {
        SaveGame s = new SaveGame();
        s.scene = SceneManager.GetActiveScene().name;
        stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
        if (steve) {
            Vector3 pos = GameObject.FindObjectOfType<stevecontroller>().transform.position;
            s.pos_x = pos.x;
            s.pos_y = pos.y;
            s.pos_z = pos.z;
        } else if (s.scene != "BloodFalline") {
            Debug.LogWarning("No steve found in " + s.scene);
        }
        s.inventory = GameManager.Instance.itemManager.ToIdList(Inventory.Instance.items);
        return s;
    }
    /*
    private static void LoadSave(SaveGame s) {
        Inventory.Instance.items = GameManager.Instance.itemManager.FromIdList(s.inventory);
        playerPos = new Vector3(s.pos_x, s.pos_y, s.pos_z);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(s.scene);
    }
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject.FindObjectOfType<stevecontroller>().transform.position
                = playerPos + new Vector3(0, 0.1f, 0);
    } */
    private static void SaveToFile(SaveGame s, string relpath = DEFAULT_SAVE_FILE) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + relpath);
        bf.Serialize(file, s);
        file.Close();
    }
    public static SaveGame LoadFromFile(string relpath = DEFAULT_SAVE_FILE) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenRead(Application.persistentDataPath + "/" + relpath);
        SaveGame s = (SaveGame) bf.Deserialize(file);
        file.Close();
        return s;
    }
    public static void Save() {
        SaveToFile(CreateSave());
    }
    /*public static void Load() {
        LoadSave(LoadFromFile());
    }*/
    public static bool SaveExists() {
        return File.Exists(Application.persistentDataPath + "/" + DEFAULT_SAVE_FILE);
    }
}
