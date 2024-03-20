using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : Ability
{

    private float originalSpeed;
    private void Awake()
    {
        ability = Effect.SPEED;
        abilityEffectTime = 3f;
    }
    public override void PerformAbility(GameObject gameObject)
    {
        originalSpeed = GameHandler.OBJECTS_MOVING_SPEED;
        GameHandler.OBJECTS_MOVING_SPEED *= 1.5f;
        Destroy(this.gameObject);
        StartCoroutine(ResetAbilityEffectAfterDelay(abilityEffectTime));
    }

    public override IEnumerator ResetAbilityEffectAfterDelay(float delay)
    {
        GetComponent<
            
            Renderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        GameHandler.OBJECTS_MOVING_SPEED = originalSpeed;
        Destroy(this.gameObject);
    }

}
