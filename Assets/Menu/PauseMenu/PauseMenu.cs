using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    private static float lastUnpaused = 0.0f;
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;
    public Button button;
    public Button optionsButton;
    public Portal mainMenu;
    private Scene menu;
    public Button retryMinigameButton;

    void Start(){
        pauseMenuUI.SetActive(false);
    }
    void Awake(){
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }

    private void PauseAudio() {
        AudioListener.pause = true;
    }

    private void PlayAudio() {
        AudioListener.pause = false;
    }

    void Update(){
        if (SceneManager.GetActiveScene().name != "MenuScene"
                && SceneManager.GetActiveScene().name != "IntroScene"
                && SceneManager.GetActiveScene().name != "OutroScene"
                && !DialogueManager.Instance.IsDialogueActive()
                && !InventoryCanvasSlots.Instance.IsShowing()
                && Input.GetKeyDown(KeyCode.Escape)) {


            retryMinigameButton.gameObject.SetActive(GameManager.Instance.IsInMinigame());

            if(paused){
                Continue();
            } else {
                Pause();
            }
        }
    }
    public void Continue() {
        lastUnpaused = Time.fixedTime;
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(false);
        PlayAudio();
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void Pause(){
        GameManager.Instance.hintUI.ClearHint();
        if (InventoryCanvasSlots.Instance.IsShowing()) {
            InventoryCanvasSlots.Instance.Hide();
        }

        pauseMenuUI.SetActive(true);
        PauseAudio();
        StartCoroutine(UIUtility.SelectButtonLater(button));
        Time.timeScale = 0.0f;
        paused = true;
    }
    public void LoadOptions(){
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
        Time.timeScale = 0.0f;
        StartCoroutine(UIUtility.SelectButtonLater(optionsButton));
        Debug.Log("Options");
    }
    public void OptionBack(){
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }
    public void QuitGame() {
        SaveLoadSystem.Save();
        Continue();
        mainMenu.TriggerTeleport();
    }
    public void ReloadScene() {
        Continue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static bool IsPausedOrJustUnpaused() {
        return paused || (Time.fixedTime - lastUnpaused) < 0.2f;
    }
}
