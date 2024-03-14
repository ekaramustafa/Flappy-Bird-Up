using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAssets;

public static class SoundManager
{

    public enum Sound{
        Jump,
        Death,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in GameAssets.GetInstance().audioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Specified Sound cannot be found : " + sound.ToString());
        return null;
    }

   
}
