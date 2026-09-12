using Unity.Netcode;
using UnityEngine;

public class CoinsWhenNoOtherAttacks : Attack
{
    private NonDamagingAttackData.LevelData levelData;

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (NonDamagingAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        transform.root.GetComponentInChildren<PlayerMetaProgression>().ChangeCoinAmount((int)(levelData.value[0] * Player.LoaclInstance.percentageTreasurGain));

        level--;
    }
}
