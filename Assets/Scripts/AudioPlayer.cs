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

    // Update is called once per frame
    void Update()
    {
        
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
