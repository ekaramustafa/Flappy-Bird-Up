using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability: MonoBehaviour
{

    protected Effect ability;
    

    
    public abstract void PerformAbility(GameObject gameObject);

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


}
