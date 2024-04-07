using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{

    private float originalJumpSpeed;
    private Bird bird;

    private void Awake()
    {
        ability = Effect.JUMP;
    }
    public override void PerformAbility(GameObject gameObject)
    {
        bird = gameObject.GetComponent<Bird>();
        originalJumpSpeed = bird.GetJumpSpeed();
        bird.SetJumpSpeed(originalJumpSpeed * 1.5f);
        StartCoroutine(ResetAbilityEffectAfterDelay(abilityEffectTime));
    }


    public override IEnumerator ResetAbilityEffectAfterDelay(float delay)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        bird.SetJumpSpeed(originalJumpSpeed);
        AbilityManager.GetInstance().RemovePerformedAbility(this.GetComponent<Ability>());
        Destroy(this.gameObject);
    }


}
