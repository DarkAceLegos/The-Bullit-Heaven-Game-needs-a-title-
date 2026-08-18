using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LightingBoltsProj : NetworkBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float lifeTime;

    [SerializeField] public GameObject prefab;

    public List<ItemList> items = new List<ItemList>();
    public Player _player;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    //Need to get when it spawns
    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;
    }

    public void Initialize(ulong playerId, int damage1, float speed1, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        lifeTime = 0;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player);

        _player = player;

        damage = (float)((damage1 + player.additiveDamageModifier) * player.percentageDamageModifier);
        speed = (speed1 + (speed1 * player.additiveProjectileSpeed)) * player.percentageProjectileSpeed;
        duration = (duration1 + (duration1 * player.additiveDuration)) * player.percentageDuration;

        rb.linearVelocity = speed * transform.up;
    }

    private void Update()
    {
        if(!IsOwner) { return;}

        //Debug.Log("I am updating I want to die");

        lifeTime += Time.deltaTime;
        if(lifeTime >= duration) 
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        foreach (ItemList i in items)
        {
            i.item.OnHit(_player, enemyHealth, prefab, i.stacks);
        }

        enemyHealth.DamageEnemy(damage);

        Die();

        //hit enemy -> deal Damage 
    }

    private void Die()
    {
        foreach (ItemList i in items)
        {
            i.item.OnDestroyed(_player, i.stacks);
        }

        NetworkObject.Despawn(false);

        NetworkObjectPool.Singleton.ReturnNetworkObject(NetworkObject, prefab);
    }
}
