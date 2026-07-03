using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChainLightingProj : NetworkBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float lifeTime;

    [SerializeField] private AttackRange attackRange;

    [SerializeField] private List<EnemyHealth> enemyHealths;

    public List<ItemList> items = new List<ItemList>();
    public Player _player;

    [SerializeField] public GameObject prefab;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    //Need to get when it spawns
    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;
        //OnHitTester onHitTester = new OnHitTester();
        //items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));
    }

    public void Initialize(ulong playerId, int damage1, float speed1, List<ItemList> _items, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        lifeTime = 0;
        enemyHealths.Clear();

        items = _items;

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

        this.GetComponent<CircleCollider2D>().radius = .5f;

        //attackRange.ExpandRadius();

        if (enemyHealths.Contains(enemyHealth)) {  return; }

        enemyHealths.Add(enemyHealth);

        transform.position = collision.transform.position;

        //attackRange.GetComponent<CircleCollider2D>().radius = 0.0001f;

        //if(collision. == attackRange.gameObject) { Debug.Log("Returned due to attack range collition"); return; }

        enemyHealth.DamageEnemy(damage);

        foreach (ItemList i in items)
        {
            i.item.OnHit(_player, enemyHealth, i.stacks);
        }

        this.GetComponent<CircleCollider2D>().radius = 1.5f;

        //transform.position = attackRange.GetClosetEnemy();

        //hit enemy -> deal Damage 
    }

    private void Die()
    {
        NetworkObject.Despawn(false);

        NetworkObjectPool.Singleton.ReturnNetworkObject(NetworkObject, prefab);
    }
}
