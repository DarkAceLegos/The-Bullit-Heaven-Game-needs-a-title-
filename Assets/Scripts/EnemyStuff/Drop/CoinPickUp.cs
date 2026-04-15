using System;
using UnityEngine;

public class CoinPickUp : MonoBehaviour, Icollectible
{
    [SerializeField] private int amountOfCoin;

    public static event Action OnCoinCollected;

    private Rigidbody2D rb;

    private bool hasTarget;
    private Vector3 targetPos;
    private float collectSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SpawnCoin(int value, Vector3 pos)
    {
        amountOfCoin = value;

        ObjectPooler.SpawnObject(gameObject, pos, Quaternion.identity, ObjectPooler.PoolType.Coins);
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

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirction = (targetPos - transform.position).normalized;

            rb.linearVelocity = new Vector2(targetDirction.x, targetDirction.y) * collectSpeed;
        }
    }

    public void SetTarget(Vector3 target, float Speed = 5f)
    {
        hasTarget = true;
        targetPos = target;
        collectSpeed = Speed;
    }
}
