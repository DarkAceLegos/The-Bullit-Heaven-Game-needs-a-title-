using Unity.Netcode;
using UnityEngine;

public class ShootUpEffectedByGravityAttack : Attack
{
    [SerializeField] private GameObject proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;
    private bool hasPlayerItems = false;

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
            var direction = Random.insideUnitCircle;
            direction.Normalize();

            var startLocal = player.transform.position;

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, Quaternion.Euler(direction));

            enemyNetworkObject.GetComponent<ShootUpEffectedByGravityProj>().Initialize(playerId, levelData.damage, levelData.speed);//*/
            enemyNetworkObject.GetComponent<ShootUpEffectedByGravityProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*var proj1 = Instantiate(proj, player.transform.position , Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
