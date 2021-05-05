using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    private Slider slider;
    private float targetProgress = 0f;
    public float FillSpeed = 0.5f;
    private void Awake(){
       slider = gameObject.GetComponent<Slider>(); 
    }

    void Start(){
       
    }
    void Update(){
        if(slider.value < targetProgress){
            slider.value += targetProgress;
        }
    }

    public void IncrementProgress(float newProgress){
       targetProgress = slider.value + newProgress/10;
    }
}
