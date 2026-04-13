using System;
using System.ComponentModel;
using Unity.Netcode;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int experience = 1;
    [SerializeField] public int coinsOnKill = 1;
    [SerializeField] private HurtSprite enemySprite;

    [SerializeField] private float currentHeath;
    [SerializeField] private ExpPickUp ExpPickUp;
    [SerializeField] private CoinPickUp CoinPickUp;

    public static Action<EnemyHealth> onEnemyKilled;

    private void Awake()
    {
        if (LevelManager.Instance.level != 0)
        {
            maxHealth = maxHealth * LevelManager.Instance.level * NetworkManager.ConnectedClients.Count;
        }
        //maxHealth = maxHealth * NetworkManager.ConnectedClients.Count;
        currentHeath = maxHealth;
    }

    public void DamageEnemy(float damage)
    {
        enemySprite.time = 0.1f;
        enemySprite.gameObject.SetActive(true);

        DealDamageRpc(damage);
    }

    [Rpc(SendTo.Server)]
    private void DealDamageRpc(float damage)
    {
        currentHeath -= damage;

        if (currentHeath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onEnemyKilled?.Invoke(this);
        //Debug.Log($"you got some exp {experience} to the level manager {LevelManager.Instance}");

        //LevelManager.Instance.AddExpRpc((experience + Player.LoaclInstance.additiveExperience) * Player.LoaclInstance.percentageExperience);
        //ExpPickUp.SpawnExp(experience, gameObject.transform.position);
        EnemyDiedRpc();

        //PlayerMetaProgression.Instance.ChangeCoinAmount(coinsOnKill);
        Destroy(gameObject);
    }

    [Rpc(SendTo.Everyone)]
    private void EnemyDiedRpc()
    {
        //add RandomNess

        ExpPickUp.SpawnExp(experience, gameObject.transform.position);
        CoinPickUp.SpawnCoin(coinsOnKill, gameObject.transform.position);
    }
}
