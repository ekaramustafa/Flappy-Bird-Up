using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{

    private void Awake()
    {
        ability = Effect.JUMP;
    }
    public override void PerformAbility(GameObject gameObject)
    {
        Bird bird = gameObject.GetComponent<Bird>();
        bird.SetJumpSpeed(bird.GetJumpSpeed() * 2);
        Destroy(this.gameObject);


    }
}
