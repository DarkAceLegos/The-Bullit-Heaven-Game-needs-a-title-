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

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);

        this.GetComponent<Collider2D>().transform.localScale = Vector3.one * levelData.cooldown; // range/area not cooldown

        if (level == 0)
        {
            //ModDataTester onHitTester = new ModDataTester();
            //items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));
        }
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player1);

        BasicAttackData.LevelData usedLevelData = levelData;

        if (enemyHealths.Count == 0)
        {
            return;
        }

        foreach (ItemList i in items)
        {
            usedLevelData = i.item.BasicAttackDataMod(player1, i.stacks, levelData);
        }

        //Debug.Log(usedLevelData.speed);

        if (lastCast + usedLevelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        //Debug.Log("trying To Spawn Chain");

        for (int i = 0; i < ((usedLevelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            var direction = GetClosetEnemy();//.normalized; //Vector2.Distance(enemyHealths[0].transform.position ,transform.position); //Random.insideUnitCircle;
            //Debug.Log(direction);
            //direction.Normalize();//*/
            //var proj1 = Instantiate(proj, player.transform.position, Quaternion.Euler(direction));
            //proj1.GetComponent<NetworkObject>().Spawn(true);

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, Quaternion.Euler(direction));

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
        { //Debug.Log("returned"); 
            return; }
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
