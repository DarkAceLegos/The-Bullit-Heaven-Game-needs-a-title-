using Unity.Netcode;
using UnityEngine;

public class IncreaseArea : Attack
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

        transform.root.GetComponent<Player>().additiveAreaModifier += levelData.value;

        //level--;
    }
}
