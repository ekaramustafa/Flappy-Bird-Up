using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    
    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    public Transform GetRandomAbility()
    {
        return abilities[UnityEngine.Random.Range(0, abilities.Count)];
    }

    public Transform GetRandomCoin()
    {
        return coins[0];
    }

    public Transform pfPipeHead;
    public Transform pfPipeBody;

    public List<Transform> abilities;
    public List<Transform> coins;


    public List<SoundAudioClip> audioClips;
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }

}
