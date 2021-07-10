using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextTimer : MonoBehaviour
{
    public Portal portalBackToMall;
    public float timeValue = 30f;
    public Text timeText;
    public Text labelText;
    private const string TIME_LABEL_DEFAULT = "Time left:";
    public Item winItem;
    public Item lostItem;
    
    private void Awake() {
        labelText.text = Uwu.OptionalUwufy(TIME_LABEL_DEFAULT);
    }

    void Update()
    {
        if(timeValue > 0f){
            timeValue -= Time.deltaTime;
        }
        else{
            timeValue=0f;
        }
        if(timeValue < 10f){
            timeText.color = Color.red;

        }
        DisplayTime(timeValue);
    }
    void DisplayTime(float stringTime){
        if(stringTime < 0){
            stringTime = 0;
            DialogueManager.Instance.SetInstantTrue();
            Inventory.Instance.RemoveItem(lostItem);
            Inventory.Instance.AddItem(winItem);
            portalBackToMall.TriggerTeleport();


        }
        float seconds = Mathf.FloorToInt(stringTime%60);
        float minutes = Mathf.FloorToInt(stringTime/60);

        timeText.text = string.Format("{1:00}",minutes,seconds);

        
    }
}
