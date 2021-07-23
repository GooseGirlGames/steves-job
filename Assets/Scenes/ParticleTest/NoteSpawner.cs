using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notesPrefab;
    public const float MaxNumberNotes = 30;
    public RectTransform rect1;
    public RectTransform rect2;
    public void SpawnNodes(){
        for (int i = 0; i < MaxNumberNotes; ++i){
            Blubb b = Instantiate(notesPrefab, parent: this.gameObject.transform).GetComponent<Blubb>();
            b.BubbleStart(rect1, rect2);
        }
    }
}
