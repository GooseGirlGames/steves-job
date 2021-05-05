using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 30f;
    public Text timeText;
    public ButtonControl button;
    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0f){
            timeValue -= Time.deltaTime;
        }
        else{
            timeValue=0f;
        }
/*         if(timeValue == 0){
            button.active = true;
        } */
        DisplayTime(timeValue);
    }
    void DisplayTime(float stringTime){
        if(stringTime < 0){
            stringTime = 0;
        }
        float milliseconds = Mathf.FloorToInt(stringTime*1000);
        float seconds = Mathf.FloorToInt(stringTime%60);
        float minutes = Mathf.FloorToInt(stringTime/60);

        timeText.text = string.Format("{0:00}:{1:00}",seconds,milliseconds);
    }
}
