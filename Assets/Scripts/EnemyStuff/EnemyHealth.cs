using System;
using System.ComponentModel;
using Unity.Netcode;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int experience = 1;

    [SerializeField] private int currentHeath;

    public static Action<EnemyHealth> onEnemyKilled;

    private void Awake()
    {
        currentHeath = maxHealth;
    }

    public void DamageEnemy(int damage)
    {
        DealDamageRpc(damage);
    }

    [Rpc(SendTo.Server)]
    private void DealDamageRpc(int damage)
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
        LevelManager.Instance.AddExpRpc(experience); 
        Destroy(gameObject);
    }
}
