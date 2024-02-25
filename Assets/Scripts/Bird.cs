using CodeMonkey;
using System;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float jumpSpeed = 40f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

    }
}
