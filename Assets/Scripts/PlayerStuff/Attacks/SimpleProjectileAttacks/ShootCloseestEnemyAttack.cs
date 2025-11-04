using Unity.Netcode;
using UnityEngine;

public class ShootCloseestEnemyAttack : Attack
{
    [SerializeField] private ShootClosetEnemyProj proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < levelData.projCount; i++)
        {
            //var direction = Random.insideUnitCircle;
            //direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position , Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
