using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour {

    public AudioClip[] ScreenMusicClips;
    public AudioSource musicSource;
    public Game_Manager game;

	
    public void ChangeSceneMusic(int index)
    {
        musicSource.clip = ScreenMusicClips[index];
        musicSource.Play();
    }
}
