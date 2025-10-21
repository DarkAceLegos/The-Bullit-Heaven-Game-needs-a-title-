using Unity.Netcode;
using UnityEngine;

public class HealWhenNoOtherAttacks : Attack
{
    private NonDamagingAttackData.LevelData levelData;

    public override void Tick(NetworkObject player)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (NonDamagingAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        Player.LoaclInstance.GetComponent<PlayerHealth>().changeHealth(levelData.value);
    }
}
