using Unity.Netcode;
using UnityEngine;

public class FollowingAttack : Attack
{
    [SerializeField] private FollowingAttackProj proj;

    private FollowingAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var AttackData = (FollowingAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = AttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for (int i = 0; i < levelData.projCount; i++)
        {
            var direction = Random.insideUnitCircle;
            direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position, Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(levelData.damage, levelData.speed, levelData.Area);//*/
        }
    }
}
