using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenuUI;
    public Button button;
    public GameObject optionsMenu;

    void Awake(){
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused){
                Continue();
            }
            else{
                Pause();
            }
        }
    }
    public void Continue(){
        pauseMenuUI.SetActive(false);
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
        Debug.Log("Options");
    }
    public void QuitGame(){
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}
