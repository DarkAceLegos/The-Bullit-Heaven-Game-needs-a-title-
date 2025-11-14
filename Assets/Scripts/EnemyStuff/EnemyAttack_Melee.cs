using Unity.Netcode;
using UnityEngine;

public class EnemyAttack_Melee : NetworkBehaviour
{
    [SerializeField] int damage = 10;

    private float lastAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("trying to attack");

        if(lastAttackTime + 1f > Time.time) return;

        //Debug.Log("we can attack now");

        if (!collision.transform.TryGetComponent(out PlayerHealth playerHealth) || !playerHealth.IsOwner)
            { return; }

        //Debug.Log("attacking");

        lastAttackTime = Time.time;
        playerHealth.changeHealth((int)((-damage) * playerHealth.GetComponent<Player>().enemyDamageModifier)); // need to check
    }
}
