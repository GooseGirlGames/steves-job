using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    Slider s;
    private void Awake() {
        s = GetComponent<Slider>();
    }
    void Start() {
        s.value = AudioListener.volume;
    }
    public void VolumeChanged() {

        Debug.Log(AudioListener.volume);
        AudioListener.volume = s.value;
    }
}
