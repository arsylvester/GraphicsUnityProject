//AudioPlayer.cs - This script runs manages all of the sounds and music in the game.
// Created by Andrew Sylvester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] float soundEffectVolume;
    [SerializeField] AudioClip winMusic;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        //audio.clip = clip;
        audio.PlayOneShot(clip, soundEffectVolume);
    }

    public void ChangeToWinMusic()
    {
        audio.Stop();
        audio.clip = winMusic;
        audio.Play();
    }
}
