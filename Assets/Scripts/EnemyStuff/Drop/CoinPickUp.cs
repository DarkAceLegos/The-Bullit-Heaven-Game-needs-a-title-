using System;
using UnityEngine;

public class CoinPickUp : MonoBehaviour, Icollectible
{
    [SerializeField] private int amountOfCoin;

    public static event Action OnCoinCollected;

    public void SpawnCoin(int value, Vector3 pos)
    {
        amountOfCoin = value;

        ObjectPooler.SpawnObject(gameObject, pos, Quaternion.identity);
    }

    private void Pickup()
    {
        ObjectPooler.ReturnObjectToPool(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out PlayerHealth playerHealth) || !playerHealth.IsOwner)
        { return; }

        Player.LoaclInstance.GetComponentInChildren<PlayerMetaProgression>().ChangeCoinAmount((int)(amountOfCoin * Player.LoaclInstance.percentageTreasurGain));

        Pickup();
    }*/

    public void Collect()
    {
        OnCoinCollected?.Invoke();

        Player.LoaclInstance.GetComponentInChildren<PlayerMetaProgression>().ChangeCoinAmount((int)(amountOfCoin * Player.LoaclInstance.percentageTreasurGain));

        Pickup();
    }

    public void Attrack(Transform transform)
    {
        gameObject.transform.Translate(transform.position);
    }
}
