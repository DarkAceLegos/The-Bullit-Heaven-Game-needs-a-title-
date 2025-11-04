using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShootClosetEnemyProj : NetworkBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb;

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

    public void Initialize(ulong playerId, int damage1, float speed1, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        //Debug.Log(Player.LoaclInstance);

        PlayerHealth._allPlayers[playerId].TryGetComponent<Player>(out var player);

        //Debug.Log(playerMetaProgression);

        damage = (float)((damage1 + player.additiveDamageModifier) * player.percentageDamageModifier);
        speed = speed1;
        duration = duration1;
        rb.linearVelocity = Random.insideUnitCircle * speed;
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

        enemyHealth.DamageEnemy(damage);

        //hit enemy -> deal Damage 
    }
}
