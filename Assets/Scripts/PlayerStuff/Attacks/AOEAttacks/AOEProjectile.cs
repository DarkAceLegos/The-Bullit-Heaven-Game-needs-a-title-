using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class AOEProjectile : NetworkBehaviour
{
    [SerializeField] private int damage;
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

    public void Initialize(int damage1, float speed1, float area1, float duration1 = 4f)
    {
        //Debug.Log("I initialized");

        damage = damage1;
        speed = speed1;
        duration = duration1;
        //rb.linearVelocity = Random.insideUnitCircle * speed;
        area = area1;
        transform.localScale = transform.localScale * area;
        Debug.Log(transform.localScale);
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
