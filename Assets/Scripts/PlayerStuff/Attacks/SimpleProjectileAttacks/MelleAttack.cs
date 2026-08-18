using Unity.Netcode;
using UnityEngine;

public class MelleAttack : Attack
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
        //Debug.Log(Direction);

        ulong playerId = player.OwnerClientId;

        player.TryGetComponent<Player>(out Player player1);

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

        Vector3 Offset = player1.direction;

        if (Direction == 0)
        { Offset = Vector3.up; }
        else if (Direction == 1)
        { Offset = Vector3.down; }
        else if (Direction == 2)
        { Offset = Vector3.left; }
        else if (Direction == 3)
        { Offset = Vector3.right; }

        for (int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        { 
            var rotation = Quaternion.identity;
            Vector3 direction = Vector3.zero;
            rotation.x = 0;
            rotation.y = 0;
            if(Direction == 0 )
            { rotation.z = 0; direction = Vector3.up; }
            else if (Direction == 1)
            { rotation.z = 0; direction = Vector3.down; }
            else if(Direction == 2 ) 
            { rotation.z = 1; direction = Vector3.left; }
            else if (Direction == 3)
            { rotation.z = 1; direction = Vector3.right; }

            var startLocal = player.transform.position;

            foreach (ItemList j in items)
            {
                proj = j.item.AttackChangeProj(player1, j.stacks, proj);
                direction = j.item.AttackDirectionMod(player1, j.stacks, direction, usedLevelData.projCount + player1.additiveProjectileModifier, i);
                startLocal = j.item.AttackStartLocationMod(player1, j.stacks, startLocal, usedLevelData.projCount + player1.additiveProjectileModifier, i);
            }

            NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, (startLocal + Offset) + (i * (direction)), rotation);

            enemyNetworkObject.GetComponent<MelleProj>().Initialize(playerId, levelData.damage, levelData.speed);
            enemyNetworkObject.GetComponent<MelleProj>().prefab = proj;

            enemyNetworkObject.Spawn(true);

            /*//direction.Normalize();
            var proj1 = Instantiate(proj, (player.transform.position + Offset) + (i * (direction)), rotation);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
