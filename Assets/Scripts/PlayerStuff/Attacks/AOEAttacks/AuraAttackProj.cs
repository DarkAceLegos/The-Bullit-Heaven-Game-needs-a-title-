using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class AuraAttackProj : NetworkBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float area;

    [SerializeField] private float lifeTime;

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

        PlayerHealth._allPlayers[playerId].TryGetComponent<Player>(out var player);

        damage = (float)((damage1 + player.additiveDamageModifier) * player.percentageDamageModifier);
        speed = speed1 ;
        duration = duration1;
        area = area1 + player.additiveAreaModifier;
        transform.localScale = transform.localScale * area;
        rb.linearVelocity = Random.insideUnitCircle * speed;
        //Debug.Log(transform.localScale);

        this.GetComponent<FollowTransform>().SetTargetTransform(player.transform);
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        //Debug.Log("I am updating I want to die");

        lifeTime += Time.deltaTime;
        if (lifeTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        enemyHealth.DamageEnemy(damage);

        //hit enemy -> deal Damage 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        enemyHealth.DamageEnemy(damage);
    }
}
