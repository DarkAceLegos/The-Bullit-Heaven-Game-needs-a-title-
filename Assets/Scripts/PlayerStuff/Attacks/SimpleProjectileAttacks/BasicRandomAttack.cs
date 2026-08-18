using NUnit.Framework;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicRandomAttack : Attack
{
    [SerializeField] private GameObject proj;
    //[SerializeField] private Collider2D range;

    //[SerializeField] private List<EnemyHealth> enemyHealths;

    private BasicAttackData.LevelData levelData;
    private float lastCast;
    private bool hasPlayerItems = false; 

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);

        //range.transform.localScale = levelData.speed;
    }

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player1);

        /*if (enemyHealths.Count == 0)
        {
            return;
        }*/

        if (!hasPlayerItems)
        {
            foreach (ItemList item in player1.items)
            {
                items.Add(item);
            }
            hasPlayerItems = true;
        }

        BasicAttackData.LevelData usedLevelData = levelData;

        foreach (ItemList i in items)
        {
            usedLevelData = i.item.BasicAttackDataMod(player1, i.stacks, levelData); // need to add to rest
        }

        if (!skipCooldown)
        {
            if (lastCast + usedLevelData.cooldown > Time.time) { return; }
            lastCast = Time.time;
        }

        foreach (ItemList i in items)
        {
            i.item.OnCast(player1, i.stacks); // need to add to rest
        }

        for (int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            Vector3 direction = Random.insideUnitSphere * 360;

            direction.x = 0;
            direction.y = 0;

            var startLocal = player.transform.position;

            foreach (ItemList j in items)
            {
                proj = j.item.AttackChangeProj(player1, j.stacks, proj);
                //direction = j.item.AttackDirectionMod(player1, j.stacks, direction, usedLevelData.projCount + player1.additiveProjectileModifier, i);
                startLocal = j.item.AttackStartLocationMod(player1, j.stacks, startLocal, usedLevelData.projCount + player1.additiveProjectileModifier, i);
            }

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, startLocal, Quaternion.Euler(direction));

            enemyNetworkObject.GetComponent<BasicRandomProj>().Initialize(playerId, levelData.damage, levelData.speed);//*/
            enemyNetworkObject.GetComponent<BasicRandomProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*var proj1 = Instantiate(proj, player.transform.position, direction);
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
