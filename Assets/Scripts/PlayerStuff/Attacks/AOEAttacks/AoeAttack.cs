using Unity.Netcode;
using UnityEngine;

public class AoeAttack : Attack
{
    [SerializeField] private GameObject proj;

    private AOEAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (AOEAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player, int Direction = 0)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out Player player1);

        AOEAttackData.LevelData usedLevelData = levelData;

        foreach (ItemList i in items)
        {
            usedLevelData = i.item.AOEAttackDataMod(player1, i.stacks, levelData); // need to add to rest
        }

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for (int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            var direction = Random.insideUnitCircle;
            direction.Normalize();

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, Quaternion.Euler(direction));

            enemyNetworkObject.GetComponent<AOEProjectile>().Initialize(playerId, levelData.damage, levelData.speed, levelData.area);
            enemyNetworkObject.GetComponent<AOEProjectile>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*var proj1 = Instantiate(proj, player.transform.position, Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed, levelData.area);//*/
        }
    }
}
