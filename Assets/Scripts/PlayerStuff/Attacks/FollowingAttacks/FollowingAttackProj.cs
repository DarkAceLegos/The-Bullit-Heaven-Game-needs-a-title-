using System.Globalization;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowingAttackProj : NetworkBehaviour
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
        speed = (speed1 + (speed1 * player.additiveProjectileSpeed)) * player.percentageProjectileSpeed;
        duration = (duration1 + (duration1 * player.additiveDuration)) * player.percentageDuration;
        area = (area1 + (area1 * player.additiveAreaModifier)) * player.percentageAreaModifier;
        transform.localScale = transform.localScale * area;

        var rotation = Random.rotation;
        rotation.x = 0;
        rotation.y = 0;
        //rotation.z = Random.Range(.5f, 1f);

        transform.rotation = rotation;

        this.GetComponent<FollowTransform>().SetTargetTransform(player.transform);
        this.GetComponent<RotateAroundATransform>().SetTargetTransform(player.transform, speed * 100);
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
}
