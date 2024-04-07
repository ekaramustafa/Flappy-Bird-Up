using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Ability: MonoBehaviour, IInteractable
{

    protected Effect ability;
    protected float abilityEffectTime = 3f;

    private protected static int counter;
    private int id;

    private void Awake()
    {
        id = counter;
        counter++;
    }

    public abstract void PerformAbility(GameObject gameObject);

    public void Interact(GameObject gameObject)
    {
        AbilityManager.GetInstance().PerformAbility(this,gameObject);
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

    public int GetID()
    {
        return id;
    }


}
