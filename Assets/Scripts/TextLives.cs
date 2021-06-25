using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextLives : MonoBehaviour
{
    public Text livesText;

    void Update()
    {
        if(BloodFalling.splatCount == 2){
            livesText.color = Color.red;
        }
        livesText.text = (3 - BloodFalling.splatCount).ToString();
    }
}
