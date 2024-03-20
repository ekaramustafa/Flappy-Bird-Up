using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        Playing,
        BirdDead
    }

    public static float BIRD_X_POSITION = 0f;

    public static State state;
    public static float _objectsMovingSpeed = 30f;
    [SerializeField]
    public static float OBJECTS_MOVING_SPEED { get
        {
            return _objectsMovingSpeed;
        }
        set
        {         
            float ratio = value / _objectsMovingSpeed;
            Level.GetInstance().ScaleSpawnerTimers(ratio);
            _objectsMovingSpeed = value;
            
        } }

    private void Awake()
    {
        state = State.WaitingToStart;
        _objectsMovingSpeed = 30f;
        Pipe.PIPES_PASSED_COUNT = 0;
    }

    private void Start()
    {
        ScoreManager.Start();
    }



}
