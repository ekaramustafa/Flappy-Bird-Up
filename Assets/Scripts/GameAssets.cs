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
        float initialProbability = 0.4f; // Setting initial probability to 30%
        float sumProbabilities = 0f;

        List<float> probabilities = new List<float>();

        for (int i = 0; i < coins.Count; i++)
        {
            float probability = i == 0 ? initialProbability : initialProbability * Mathf.Pow(1 - initialProbability, i);
            probabilities.Add(probability);
            sumProbabilities += probability;
        }

        // Normalize probabilities
        for (int i = 0; i < probabilities.Count; i++)
        {
            probabilities[i] /= sumProbabilities;
        }

        float randomVal = UnityEngine.Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        for (int i = 0; i < coins.Count; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomVal < cumulativeProbability)
            {
                return coins[i];
            }
        }
        return coins[0];
    }

    public Transform pfPipeHead;
    public Transform pfPipeBody;

    public List<Transform> abilities;
    public List<Transform> coins;  //This is priority-based List
    


    public List<SoundAudioClip> audioClips;
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }

}
