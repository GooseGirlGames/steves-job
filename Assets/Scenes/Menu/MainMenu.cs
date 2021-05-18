using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void game_start(){

    }


    public void close_game(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
