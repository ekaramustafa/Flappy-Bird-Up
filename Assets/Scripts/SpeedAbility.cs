using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : Ability
{


    private void Awake()
    {
        ability = Effect.SPEED;
    }
    public override void PerformAbility(GameObject gameObject)
    {
        GameHandler.OBJECTS_MOVING_SPEED *= 1.5f;
        Destroy(this.gameObject);
    }

}