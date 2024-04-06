using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability: MonoBehaviour, IInteractable
{

    protected Effect ability;
    public float abilityEffectTime;

    private protected static int id;

    private void Awake()
    {
        abilityEffectTime = 2f;
        id++;
    }

    public abstract void PerformAbility(GameObject gameObject);

    public void Interact(GameObject gameObject)
    {
        //AbilityManager.GetInstance().PerformAbility(this,gameObject);
        PerformAbility(gameObject);
    }


    public abstract IEnumerator ResetAbilityEffectAfterDelay(float delay);

    public void Move()
    {
        gameObject.transform.position += new Vector3(-1, 0, 0) * GameHandler.OBJECTS_MOVING_SPEED * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pipe")){
            Destroy(gameObject);
        }
    }


    public enum Effect
    {
        SPEED,
        MASS,
        IMMORTALITY,
        JUMP,
    }

    private void Update()
    {
        if (GameHandler.state == GameHandler.State.Playing)
        {
            Move();
        }
    }

    public float GetAbilityEffectTime()
    {
        return abilityEffectTime;
    }


}
