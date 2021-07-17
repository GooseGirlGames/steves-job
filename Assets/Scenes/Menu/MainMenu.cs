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
    public Slider volumeSlider;
    
    void Awake(){
        StartCoroutine(UIUtility.SelectButtonLater(button));
    }

    public void game_start(){

    }

    public void DeleteSave() {
        SaveLoadSystem.DeleteSave();
        GameObject.FindObjectOfType<menucontroller>().bye();
        Bye();
    }

    public void OnVolumeChanged() {
        AudioListener.volume = volumeSlider.value;
    }
    public void OptionBackButton(){
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        StartCoroutine(UIUtility.SelectButtonLater(button));
        GameObject.FindObjectOfType<ContinueButton>().UpdateSave();
    }
    
    public void LoadOptions(){
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        volumeSlider.value = AudioListener.volume;
        StartCoroutine(UIUtility.SelectButtonLater(optionsButton));
    }

    private IEnumerator Bye() {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }    
    public void close_game(){
        StartCoroutine(Bye());
        Debug.Log("Quit");
    }
}
