using Unity.Netcode;
using UnityEngine;

public class CoinsWhenNoOtherAttacks : Attack
{
    private NonDamagingAttackData.LevelData levelData;

    public override void Tick(NetworkObject player, int Direction = 0)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (NonDamagingAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        level--;
    }
}
