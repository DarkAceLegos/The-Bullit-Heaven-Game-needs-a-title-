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

        PlayerHealth._allPlayers[playerId].TryGetComponent<Player>(out var player);

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
            Destroy(gameObject);
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

        this.GetComponent<CircleCollider2D>().radius = 1.5f;

        //transform.position = attackRange.GetClosetEnemy();

        //hit enemy -> deal Damage 
    }
}
