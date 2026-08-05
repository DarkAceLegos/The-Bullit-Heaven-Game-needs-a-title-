using Unity.Netcode;
using UnityEngine;

public class DirctionAttack : Attack
{
    [SerializeField] private GameObject proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player, int Direction = 0)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player1);

        BasicAttackData.LevelData usedLevelData = levelData;

        foreach (ItemList i in items)
        {
            usedLevelData = i.item.BasicAttackDataMod(player1, i.stacks, levelData); // need to add to rest
        }

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            var rotation = Quaternion.identity;
            Vector3 direction = Vector3.zero;
            rotation.x = 0;
            rotation.y = 0;
            if (Direction == 0)
            { rotation.z = 0; direction = Vector3.up; }
            else if (Direction == 1)
            { rotation.z = -180; direction = Vector3.down; }
            else if (Direction == 2)
            { rotation.z = 1; direction = Vector3.left; }
            else if (Direction == 3)
            { rotation.z = -1; direction = Vector3.right; }

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, rotation);

            enemyNetworkObject.GetComponent<DirectionProj>().Initialize(playerId, levelData.damage, levelData.speed);
            enemyNetworkObject.GetComponent<DirectionProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
