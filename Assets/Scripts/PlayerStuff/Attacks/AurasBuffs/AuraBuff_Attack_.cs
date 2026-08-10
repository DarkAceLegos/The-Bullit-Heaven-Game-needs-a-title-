using Unity.Netcode;
using UnityEngine;

public class AuraBuff_Attack_ : Attack
{
    [SerializeField] private GameObject proj;

    private AuraBuffAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (AuraBuffAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out Player player1);

        AuraBuffAttackData.LevelData usedLevelData = levelData;

        foreach (ItemList i in items)
        {
            //usedLevelData = i.item.(player1, i.stacks, levelData); // need to add to rest
        }

        if (!skipCooldown)
        {
            if (lastCast + usedLevelData.cooldown > Time.time) { return; }
            lastCast = Time.time;
        }

        for (int i = 0; i < levelData.projCount; ++i) //+ player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            var direction = Random.insideUnitCircle;
            direction.Normalize();

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, Quaternion.identity);

            enemyNetworkObject.GetComponent<AuraBuff_Proj_>().Initialize(playerId, levelData.amount, levelData.speed, levelData.area, levelData.stat);
            enemyNetworkObject.GetComponent<AuraBuff_Proj_>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*var proj1 = Instantiate(proj, player.transform.position, Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.amount, levelData.speed, levelData.area, levelData.stat);//*/
        }
    }
}
