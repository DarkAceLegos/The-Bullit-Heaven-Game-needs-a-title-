using Unity.Netcode;
using UnityEngine;

public class IncreaseStat : Attack
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

        var player = transform.root.GetComponent<Player>();//.additiveAreaModifier += levelData.value;

        switch (levelData.stat)
        {
            case PlayerBaseStats.Stat.additiveMaxHealthModifier: player.additiveMaxHealthModifier += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageMaxHealthModifier: player.percentageMaxHealthModifier += levelData.value; break;
            case PlayerBaseStats.Stat.additiveDamageModifier: player.additiveDamageModifier += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageDamageModifier: player.percentageDamageModifier += levelData.value; break;
            case PlayerBaseStats.Stat.percentageCooldownModifier: player.percentageCooldownModifier += levelData.value; break;
            case PlayerBaseStats.Stat.additiveProjectileModifier: player.additiveProjectileModifier += (int)levelData.value; break;
            case PlayerBaseStats.Stat.additiveAreaModifier: player.additiveAreaModifier += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageAreaModifier: player.percentageAreaModifier += levelData.value; break;
            case PlayerBaseStats.Stat.enemySpawnModifier: player.enemySpawnModifier += levelData.value; break;
            case PlayerBaseStats.Stat.enemyDamageModifier: player.enemyDamageModifier += levelData.value; break;
            case PlayerBaseStats.Stat.playerHealthRegen: player.playerHealthRegen += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentagePlayerHealthRegen: player.percentagePlayerHealthRegen += levelData.value; break;
            case PlayerBaseStats.Stat.percentageTreasureFind: player.percentageTreasureFind += levelData.value; break;
            case PlayerBaseStats.Stat.percentageTreasurGain: player.percentageTreasurGain += levelData.value; break;
            case PlayerBaseStats.Stat.additivePlayerMoveSpeed: player.additivePlayerMoveSpeed += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentagePlayerMoveSpeed: player.percentagePlayerMoveSpeed += levelData.value; break;
            case PlayerBaseStats.Stat.additiveProjectileSpeed: player.additiveProjectileSpeed += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageProjectileSpeed: player.percentageProjectileSpeed += levelData.value; break;
            case PlayerBaseStats.Stat.additiveDuration: player.additiveDuration += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageDuration: player.percentageDuration += levelData.value; break;
            case PlayerBaseStats.Stat.additiveExperience: player.additiveExperience += (int)levelData.value; break;
            case PlayerBaseStats.Stat.percentageExperience: player.percentageExperience += levelData.value; break;
                //case PlayerBaseStats.Stat.


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }

        //level--;
    }
}
