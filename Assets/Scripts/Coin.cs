using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{


    [SerializeField]
    CoinSO coinSO;
    public void Interact(GameObject gameObject)
    {
        CoinManager.AddCoin(coinSO.value);
        SoundManager.PlaySound(SoundManager.Sound.CoinCollect);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        gameObject.transform.position += new Vector3(-1, 0, 0) * GameHandler.OBJECTS_MOVING_SPEED * Time.deltaTime;
    }
}
