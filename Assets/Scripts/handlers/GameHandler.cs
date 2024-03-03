using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{

    [SerializeField]
    public static float OBJECTS_MOVING_SPEED { get; set; } = 30f;

    private void Awake()
    {
        OBJECTS_MOVING_SPEED = 30f;
    }



}
