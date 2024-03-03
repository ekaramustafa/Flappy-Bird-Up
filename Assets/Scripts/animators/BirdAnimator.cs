using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private const string IS_PLAYING = "IsPlaying";

    private Animator animator;


    private Bird bird;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        bird = GetComponent<Bird>();
    }
    void Start()
    {
        bird.OnDied += BirdAnimator_OnDied;
        bird.OnStartedPlaying += Bird_OnStartedPlaying;
    }

    private void Bird_OnStartedPlaying(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_PLAYING, true);
    }

    private void BirdAnimator_OnDied(object sender, System.EventArgs e)
    {
        animator.StopPlayback();
        animator.SetBool(IS_PLAYING,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
