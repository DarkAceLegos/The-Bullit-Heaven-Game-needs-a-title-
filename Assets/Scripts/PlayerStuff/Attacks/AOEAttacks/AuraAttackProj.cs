using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class AuraAttackProj : NetworkBehaviour//, INetworkPrefabInstanceHandler
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float area;
    [SerializeField] private float attackInterval = 1f;

    [SerializeField] private float lifeTime;
    [SerializeField] private List<EnemyHealth> enemyHealths;
    //private Player playerStored;

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

    public void Initialize(ulong playerId, int damage1, float speed1, float area1, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        lifeTime = 0;
        enemyHealths.Clear();

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player);
        _player = player;

        damage = (float)((damage1 + player.additiveDamageModifier) * player.percentageDamageModifier);
        //speed = (speed1 + (speed1 * player.additiveProjectileSpeed)) * player.percentageProjectileSpeed;
        duration = (duration1 + (duration1 * player.additiveDuration)) * player.percentageDuration;
        area = (area1 + (area1 *  player.additiveAreaModifier)) * player.percentageAreaModifier;
        attackInterval = .5f / player.percentageCooldownModifier;
        transform.localScale = transform.localScale * area;
        rb.linearVelocity = Random.insideUnitCircle * speed;
        //Debug.Log(transform.localScale);

        

        GetComponent<FollowTransform>().SetTargetTransform(player.transform);
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        //Debug.Log("I am updating I want to die");

        attackInterval -= Time.deltaTime;

        lifeTime += Time.deltaTime;
        if (lifeTime >= duration)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        foreach (ItemList i in items)
        {
            i.item.OnHit(_player, enemyHealth, i.stacks);
        }

        enemyHealths.Add(enemyHealth);

        //enemyHealth.DamageEnemy(damage);

        //hit enemy -> deal Damage 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }
        
        if(!(attackInterval < 0f)) { return; }

        foreach (var enemy in enemyHealths)
        {
            enemy.DamageEnemy(damage);
        }

        attackInterval = .5f / _player.percentageCooldownModifier;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        enemyHealths.Remove(enemyHealth);
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
