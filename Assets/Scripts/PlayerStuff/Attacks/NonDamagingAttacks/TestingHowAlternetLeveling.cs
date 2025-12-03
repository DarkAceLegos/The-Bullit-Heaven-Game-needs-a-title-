using Unity.Netcode;
using UnityEngine;

public class TestingHowAlternetLeveling : Attack
{
    private NonDamagingAttackData.LevelData levelData;

    [SerializeField] private Attack attack;

    public override void Tick(NetworkObject player)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (NonDamagingAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        Player.LoaclInstance.GetComponentInChildren<AoeAttack>();

        level--;
    }
}
