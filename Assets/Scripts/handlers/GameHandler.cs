using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    public enum State
    {
        CosmeticSelection,
        WaitingToStart,
        Playing,
        BirdDead
    }

    public static float BIRD_X_POSITION = 0f;
    public const float CAMERA_ORTHO_SIZE = 50f;


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
        state = State.CosmeticSelection;
        _objectsMovingSpeed = 30f;
        Pipe.PIPES_PASSED_COUNT = 0;
    }

    private void Start()
    {
        ScoreManager.Start();
    }



}
