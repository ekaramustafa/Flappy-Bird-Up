using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private const string IS_PLAYING = "IsPlaying";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        GetComponent<Bird>().OnDied += BirdAnimator_OnDied;
        GetComponent<Bird>().OnStartedPlaying += Bird_OnStartedPlaying;
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

}
