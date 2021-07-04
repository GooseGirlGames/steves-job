using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;
    public Button button;
    public Button optionsButton;
    public Portal mainMenu;
    private Scene menu;

    void Start(){
        pauseMenuUI.SetActive(false);
    }
    void Awake(){
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }

    void Update(){
        if(SceneManager.GetActiveScene().name != "MenuScene"){
            if(Input.GetKeyDown(KeyCode.Escape)){
                if(paused){
                    Continue();
                }
                else{
                    Pause();
                }
            }
        }
    }
    public void Continue(){
        
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        StartCoroutine(UIUtility.SelectButtonLater(button));
        Time.timeScale = 0.0f;
        paused = true;
    }
    public void LoadOptions(){
        optionsMenu.SetActive(true);
        Time.timeScale = 0.0f;
        StartCoroutine(UIUtility.SelectButtonLater(optionsButton));
        Debug.Log("Options");
    }
    public void OptionBack(){
        optionsMenu.SetActive(false);
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }
    public void QuitGame(){
        mainMenu.TriggerTeleport();
    }
}
