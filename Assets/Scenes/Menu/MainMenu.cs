using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button button;
    public Button optionsButton;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    
    void Awake(){
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }

    public void game_start(){

    }

    public void OptionBackButton(){
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }
    
    public void LoadOptions(){
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        
        StartCoroutine(UIUtility.SelectButtonLater(optionsButton));
    }
    
    public void close_game(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
