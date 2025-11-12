using Unity.Netcode;
using UnityEngine;

public class BouncingProjAttack : Attack
{
    [SerializeField] private BounceingProj proj;

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
            var direction = Random.rotation;
            direction.x = 0;
            direction.y = 0;
            //Debug.Log(direction);
            direction.Normalize();//*/
            var proj1 = Instantiate(proj, player.transform.position, direction);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
