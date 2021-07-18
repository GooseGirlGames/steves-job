using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdrMusic : MonoBehaviour {
    public AudioClip music;
    public AudioClip shortMusic;
    public AudioClip halfRestoredMusic;
    public AudioClip won;
    public AudioSource audioSource;
    private AudioSource currentAudio;
    public Item _can_use_ddr;
    public Item _restoredSteveEHorror;
    public Item _restoredCuteE;
    public Item _powered;
    private bool playMusic = false;
    private bool playShortMusic = false;
    private bool playHalfRestoredMusic = false;
    private bool playWonMusic = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Inventory.Instance.HasItem(_restoredSteveEHorror) || Inventory.Instance.HasItem(_restoredCuteE)){
            playHalfRestoredMusic = true;
            playMusic = false;
            playShortMusic = false;
            playWonMusic = false;
        }
        if (Inventory.Instance.HasItem(_restoredSteveEHorror) && Inventory.Instance.HasItem(_restoredCuteE)){
            playHalfRestoredMusic = false;
            playMusic = true;
            playShortMusic = false;
            playWonMusic = false;
        }
        if (Inventory.Instance.HasItem(_can_use_ddr)){
            playHalfRestoredMusic = false;
            playMusic = false;
            playShortMusic = true;
            playWonMusic = false;
        }
        if (Inventory.Instance.HasItem(_powered)){
            playHalfRestoredMusic = false;
            playMusic = false;
            playShortMusic = false;
            playWonMusic = true;
        }
        if (playHalfRestoredMusic && !audioSource.isPlaying){
            audioSource.clip = halfRestoredMusic;
            audioSource.Play(); 
            if (playMusic && audioSource.isPlaying){
                audioSource.Stop();
            }
        }
        
        if (playMusic && !audioSource.isPlaying){
            audioSource.clip = music;
            audioSource.Play();
            if (playShortMusic && audioSource.isPlaying){
                audioSource.Stop();
            }
        }
        
        if (playShortMusic && !audioSource.isPlaying) {
            audioSource.loop = false;
            audioSource.clip = shortMusic;
            audioSource.Play();
            
            if (playWonMusic && audioSource.isPlaying){
                audioSource.Stop();
            }
        }
        
        if (playWonMusic && !audioSource.isPlaying){
            audioSource.loop = false;
            audioSource.clip = won;
            audioSource.Play();
        } 

    }
}