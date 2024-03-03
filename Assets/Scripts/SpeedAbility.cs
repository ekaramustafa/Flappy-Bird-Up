using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : Ability
{
    public override void PerformAbility(GameObject gameObject)
    {
        Bird bird = gameObject.GetComponent<Bird>();
        bird.SetJumpSpeed(bird.GetJumpSpeed() * 2);
        Destroy(this.gameObject);
    }

}
