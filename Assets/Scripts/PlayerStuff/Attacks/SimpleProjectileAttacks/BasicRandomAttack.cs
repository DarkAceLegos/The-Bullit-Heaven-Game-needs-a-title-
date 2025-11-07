using NUnit.Framework;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicRandomAttack : Attack
{
    [SerializeField] private BasicRandomProj proj;
    //[SerializeField] private Collider2D range;

    //[SerializeField] private List<EnemyHealth> enemyHealths;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);

        //range.transform.localScale = levelData.speed;
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        /*if (enemyHealths.Count == 0)
        {
            return;
        }*/

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < levelData.projCount; i++)
        {
            var direction = Random.rotation;
            direction.x = 0;
            direction.y = 0;
            //Debug.Log(direction);
            direction.Normalize();//*/
            var proj1 = Instantiate(proj, player.transform.position, direction);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        if(enemyHealths.Contains(enemyHealth)) { return; }

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

    private Vector3 GetClosetEnemy()
    {
        float closestDistant = float.MaxValue;
        EnemyHealth closestEnemyHealth = default;
        
        foreach (var enemy in enemyHealths)
        {
            if (enemy == null) { enemyHealths.Remove(enemy); }

            var enemyPos = enemy.transform.position;
            var distance = Vector2.Distance(enemyPos , transform.position);
            if (distance < closestDistant)
            {
                closestDistant = distance;
                closestEnemyHealth = enemy;
            }
        }

        //Debug.Log((Vector3.Angle(transform.position - closestEnemyHealth.transform.position, Vector2.up)));
        Vector3 returnVector = new Vector3(0, 0, -(Vector2.SignedAngle(closestEnemyHealth.transform.position - transform.position, Vector2.up)));
        return returnVector;
    }*/
}
