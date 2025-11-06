using NUnit.Framework;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BasicRandomAttack : Attack
{
    [SerializeField] private BasicRandomProj proj;
    //[SerializeField] private Collider2D range;

    [SerializeField] private List<EnemyHealth> enemyHealths;

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

        if (enemyHealths.Count == 0)
        {
            return;
        }

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < levelData.projCount; i++)
        {
            var direction = GetClosetEnemy();//.normalized; //Vector2.Distance(enemyHealths[0].transform.position ,transform.position); //Random.insideUnitCircle;
            Debug.Log(direction);
            direction.Normalize();//*/
            var proj1 = Instantiate(proj, player.transform.position , Quaternion.Euler(0,0,0));
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        Debug.Log("adding a enemy to the list");

        enemyHealths.Add(enemyHealth);
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

        Debug.Log((closestEnemyHealth.transform.position - transform.position).normalized);
        return (closestEnemyHealth.transform.position - transform.position);
    }
}
