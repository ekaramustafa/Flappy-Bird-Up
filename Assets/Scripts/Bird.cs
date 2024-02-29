using CodeMonkey;
using System;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private State state;

    public event EventHandler OnDied;
    public event EventHandler OnstartedPlaying;

    [SerializeField]
    private float jumpSpeed = 40f;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    OnstartedPlaying?.Invoke(this, EventArgs.Empty);
                    state = State.Playing;
                    rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case State.Dead:
                rigidbody2D.bodyType = RigidbodyType2D.Static;
                break;

        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidbody2D.velocity = Vector2.up * jumpSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDied?.Invoke(this, EventArgs.Empty);
        state = State.Dead;

    }

    public bool IsPlaying()
    {
        return state == State.Playing;
    }
}
