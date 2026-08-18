using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LightingBoltsAttack : Attack
{
    [SerializeField] private GameObject proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;
    private bool hasPlayerItems = false;

    [SerializeField] private List<EnemyHealth> enemyHealths;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player1);

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

        if (enemyHealths.Count == 0)
        {
            return;
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
            int randomInt = Random.Range(0,enemyHealths.Count);
            //var direction = Random.insideUnitCircle;
            //direction.Normalize();

            Vector3 startLocal = enemyHealths[randomInt].transform.position;

            foreach (ItemList j in items)
            {
                proj = j.item.AttackChangeProj(player1, j.stacks, proj);
                //direction = j.item.AttackDirectionMod(player1, j.stacks, direction, usedLevelData.projCount + player1.additiveProjectileModifier, i);
                startLocal = j.item.AttackStartLocationMod(player1, j.stacks, startLocal, usedLevelData.projCount + player1.additiveProjectileModifier, i);
            }

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, startLocal, Quaternion.identity);

            enemyNetworkObject.GetComponent<LightingBoltsProj>().Initialize(playerId, levelData.damage, levelData.speed);
            enemyNetworkObject.GetComponent<LightingBoltsProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*var proj1 = Instantiate(proj, enemyHealths[randomInt].transform.position , Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }

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
}
