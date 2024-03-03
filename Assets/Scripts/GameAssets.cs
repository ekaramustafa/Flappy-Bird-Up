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

    public Transform pfPipeHead;
    public Transform pfPipeBody;

    public Transform ability;

}
