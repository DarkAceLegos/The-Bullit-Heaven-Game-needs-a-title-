using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AuraBuff_Proj_ : NetworkBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float area;
    [SerializeField] public AuraBuffHolder.Stat stat;

    [SerializeField] private float lifeTime;
    [SerializeField] private List<PlayerHealth> enemyHealths;
    //private Player playerStored;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    //Need to get when it spawns
    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;
    }

    public void Initialize(ulong playerId, int amount1, float speed1, float area1, AuraBuffHolder.Stat stat1, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player);
        //playerStored = player;

        amount = amount1; //+ player.additiveDamageModifier) * player.percentageDamageModifier;
        //speed = (speed1 + (speed1 * player.additiveProjectileSpeed)) * player.percentageProjectileSpeed;
        duration = duration1;// + (duration1 * player.additiveDuration)) * player.percentageDuration;
        area = (area1); //+ (area1 * player.additiveAreaModifier)) * player.percentageAreaModifier;
        stat = stat1;
        transform.localScale = transform.localScale * area;
        rb.linearVelocity = Random.insideUnitCircle * speed;
        //Debug.Log(transform.localScale);

        GetComponent<FollowTransform>().SetTargetTransform(player.transform);
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        //Debug.Log("I am updating I want to die");

        //attackInterval -= Time.deltaTime;

        lifeTime += Time.deltaTime;
        if (lifeTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collided with " + collision.gameObject);

        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out PlayerHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        enemyHealth.transform.root.GetComponent<AuraBuffHolder>().AuraBuff(stat, amount);

        enemyHealths.Add(enemyHealth);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out PlayerHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        enemyHealth.transform.root.GetComponent<AuraBuffHolder>().AuraBuff(stat, -amount);

        enemyHealths.Remove(enemyHealth);
    }
}
