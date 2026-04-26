using Unity.Netcode;
using UnityEngine;

public class AuraBuff_Attack_ : Attack
{
    [SerializeField] private AuraBuff_Proj_ proj;

    private AuraBuffAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (AuraBuffAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player)
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
            proj1.Initialize(playerId, levelData.amount, levelData.speed, levelData.area, levelData.stat);//*/
        }
    }
}
