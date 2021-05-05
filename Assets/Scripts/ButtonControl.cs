using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public void bye(){
        Application.Quit();
        Debug.Log("Game is byebye");
    }
}
