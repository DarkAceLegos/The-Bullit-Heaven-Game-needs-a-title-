using Unity.Burst.Intrinsics;
using Unity.Netcode;
using UnityEngine;

public class IncreaseAreaAura : Attack
{
    [SerializeField] private IncreaseAreaAuraProj proj;
    [SerializeField] private GameObject art;

    private AOEAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (AOEAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);

        transform.root.TryGetComponent<Player>(out Player player);

        art.transform.localScale = Vector3.one * ((levelData.area + player.additiveAreaModifier) * player.percentageAreaModifier);
    }

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out Player player1);

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for (int i = 0; i < levelData.projCount; ++i) //+ player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        {
            var direction = Random.insideUnitCircle;
            direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position, Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed, levelData.area);//*/
        }
    }
}
