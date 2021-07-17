using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdrMusic : MonoBehaviour {
    public AudioClip music;
    public AudioClip shortMusic;
    public AudioClip halfRestoredMusic;
    public AudioSource audioSource;
    public Item _can_use_ddr;
    public Item _restoredSteveEHorror;
    public Item _restoredCuteE;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        if (Inventory.Instance.HasItem(_can_use_ddr)){
            audioSource.PlayOneShot(shortMusic, 0.1f);
        }
        if (Inventory.Instance.HasItem(_restoredSteveEHorror) || Inventory.Instance.HasItem(_restoredCuteE)){
            audioSource.PlayOneShot(halfRestoredMusic, 0.1f);
        }
        if (Inventory.Instance.HasItem(_restoredSteveEHorror) && Inventory.Instance.HasItem(_restoredCuteE)){
            audioSource.PlayOneShot(music, 0.1f);
        }
        
    }
}
