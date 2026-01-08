using Unity.Netcode;
using UnityEngine;

public class AuraAttackAddingLevels : Attack
{
    [SerializeField] AOEAttackData attackDataToBeChanged;

    private AOEAttackData.LevelData levelData;

    public override void Tick(NetworkObject player)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (AOEAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        var levelDataToChange = attackDataToBeChanged.GetLevelData(level);

        levelData.projCount += levelDataToChange.projCount;
        levelData.damage += levelDataToChange.damage;
        levelData.cooldown += levelDataToChange.cooldown;
        levelData.speed += levelDataToChange.speed;
        levelData.area += levelDataToChange.area;

        attackDataToBeChanged.AddALevelData(levelData);

        AttackHandler.LoaclInstance.addAttack(attackDataToBeChanged);

        level--;
    }
}
