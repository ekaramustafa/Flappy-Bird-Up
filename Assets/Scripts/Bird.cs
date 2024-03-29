using CodeMonkey;
using System;
using UnityEditor;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    [SerializeField] private float jumpSpeed = 40f;
    

    [SerializeField] private BirdSO birdSO;
    [SerializeField] Transform hatHolderTransform;

    private Sprite sprite;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        //decompose the BirdSO
        DecomposeSO();
    }

    private void Update()
    {

        switch (GameHandler.state)
        {
            case GameHandler.State.CosmeticSelection:
                break;
            case GameHandler.State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    OnStartedPlaying?.Invoke(this, EventArgs.Empty);
                    GameHandler.state = GameHandler.State.Playing;
                    rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                }
                break;
            case GameHandler.State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case GameHandler.State.BirdDead:
                rigidbody2D.bodyType = RigidbodyType2D.Static;
                break;
        }
    }

    
    private void LateUpdate()
    {
        if(GameHandler.state != GameHandler.State.Playing)
            GetComponent<SpriteRenderer>().sprite = birdSO.sprite;
    }
    

    public void DecomposeSO()
    {
        GetComponent<SpriteRenderer>().sprite = birdSO.sprite;
        GetComponent<Animator>().runtimeAnimatorController = birdSO.runtimeAnimatorController;
        GetComponent<CircleCollider2D>().radius = birdSO.radius;
        gameObject.transform.localScale = new Vector3(birdSO.size, birdSO.size, 1);
        rigidbody2D.mass = birdSO.mass;
    }

    public void SetBirdSO(BirdSO birdSO)
    {
        this.birdSO = birdSO;
        DecomposeSO();
    }

    private void Jump()
    {
        SoundManager.PlaySound(SoundManager.Sound.Jump);
        // Preserve the horizontal velocity
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            GameObject collidedObject = collision.gameObject;
            IInteractable interactable = collidedObject.GetComponent<IInteractable>();
            interactable.Interact(this.gameObject);
        }
        else
        {
            OnDied?.Invoke(this, EventArgs.Empty);
            GameHandler.state = GameHandler.State.BirdDead;
            SoundManager.PlaySound(SoundManager.Sound.Death);
        }

    }

    public void SetJumpSpeed(float value)
    {
        jumpSpeed = value;
    }

    public float GetJumpSpeed()
    {
        return jumpSpeed;
    }


    public Transform GetHatHolderTransform()
    {
        return hatHolderTransform;
    }

    public BirdSO GetBirdSO()
    {
        return birdSO;
    }
}
