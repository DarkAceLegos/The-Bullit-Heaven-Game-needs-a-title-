using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{

    public void ExpandRadius()
    {
        this.GetComponent<CircleCollider2D>().radius += 0.1f;
    }

    /*[SerializeField] private List<EnemyHealth> enemyHealths;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        if (enemyHealths.Contains(enemyHealth)) { return; }

        Debug.Log("adding a enemy to the list");

        enemyHealths.Add(enemyHealth);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("collision exit");
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { Debug.Log("returned"); return; }
        //collision.transform.TryGetComponent(out EnemyHealth enemyHealth);
        enemyHealths.Remove(enemyHealth);
    }

    public Vector3 GetClosetEnemy()
    {
        float closestDistant = float.MaxValue;
        EnemyHealth closestEnemyHealth = default;

        foreach (var enemy in enemyHealths)
        {
            if (enemy == null) { enemyHealths.Remove(enemy); }

            var enemyPos = enemy.transform.position;
            var distance = Vector2.Distance(enemyPos, transform.position);
            if (distance < closestDistant)
            {
                closestDistant = distance;
                closestEnemyHealth = enemy;
            }
        }

        //Debug.Log((Vector3.Angle(transform.position - closestEnemyHealth.transform.position, Vector2.up)));
        Vector3 returnVector = new Vector3(0, 0, -(Vector2.SignedAngle(closestEnemyHealth.transform.position - transform.position, Vector2.up)));
        return closestEnemyHealth.transform.position;
    }*/
}
