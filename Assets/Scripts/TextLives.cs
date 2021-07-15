using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextLives : MonoBehaviour
{
    public Text livesText;
    public Text livesLabel;
    public const string LIVES_LABEL_DEFAULT = "Lives Left:";

    private void Awake() {
        livesLabel.text = Uwu.OptionalUwufy(LIVES_LABEL_DEFAULT);    
    }

    void Update()
    {
        if(BloodFalling.splatCount == 2){
            livesText.color = Color.red;
        }
        livesText.text = Mathf.Max(0, 3 - BloodFalling.splatCount).ToString();
    }
}
