using System.Collections.Generic;
using Unity.Netcode;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;

public class ChainLightiningAttack : Attack
{
    [SerializeField] private GameObject proj;

    [SerializeField] private List<EnemyHealth> enemyHealths;

    private BasicAttackData.LevelData levelData;
    private float lastCast;
    private bool hasPlayerItems = false;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);

        this.GetComponent<Collider2D>().transform.localScale = Vector3.one * levelData.cooldown; // range/area not cooldown

        /*if (level == 0)
        {
            OnHitTester onHitTester = new OnHitTester();
            items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));
        }//*/
    }

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player1);

        if(!hasPlayerItems)
        {
            foreach(ItemList item in player1.items)
            {
                items.Add(item);
            }
            hasPlayerItems = true;
        }

        BasicAttackData.LevelData usedLevelData = levelData;

        if (enemyHealths.Count == 0)
        {
            return;
        }

        foreach (ItemList i in items)
        {
            usedLevelData = i.item.BasicAttackDataMod(player1, i.stacks, levelData); // need to add to rest
        }

        //Debug.Log(usedLevelData.speed);
        if (!skipCooldown) 
        { 
            if (lastCast + usedLevelData.cooldown > Time.time) { return; }
            lastCast = Time.time; 
        }

        foreach (ItemList i in items)
        {
            i.item.OnCast(player1, i.stacks); // need to add to rest
        }

        //Debug.Log("trying To Spawn Chain");

        for (int i = 0; i < ((usedLevelData.projCount + player1.additiveProjectileModifier)); i++)
        {
            var direction = GetClosetEnemy();

            var startLocal = player.transform.position;

            foreach (ItemList j in items)
            {
                proj = j.item.AttackChangeProj(player1, j.stacks, proj);
                direction = j.item.AttackDirectionMod(player1, j.stacks, direction, usedLevelData.projCount + player1.additiveProjectileModifier, i);
                startLocal = j.item.AttackStartLocationMod(player1, j.stacks, startLocal, usedLevelData.projCount + player1.additiveProjectileModifier, i);
            }

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, startLocal, Quaternion.Euler(direction));

            enemyNetworkObject.GetComponent<ChainLightingProj>().Initialize(playerId, usedLevelData.damage, usedLevelData.speed, items);//*/
            enemyNetworkObject.GetComponent<ChainLightingProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        if (enemyHealths.Contains(enemyHealth)) { return; }

        //Debug.Log("adding a enemy to the list");

        enemyHealths.Add(enemyHealth);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("collision exit");
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { 
            //Debug.Log("returned"); 
            return; 
        }
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
            var distance = Vector2.Distance(enemyPos, transform.position);
            if (distance < closestDistant)
            {
                closestDistant = distance;
                closestEnemyHealth = enemy;
            }
        }

        //Debug.Log((Vector3.Angle(transform.position - closestEnemyHealth.transform.position, Vector2.up)));
        Vector3 returnVector = new Vector3(0, 0, -(Vector2.SignedAngle(closestEnemyHealth.transform.position - transform.position, Vector2.up)));
        return returnVector;
    }
}
