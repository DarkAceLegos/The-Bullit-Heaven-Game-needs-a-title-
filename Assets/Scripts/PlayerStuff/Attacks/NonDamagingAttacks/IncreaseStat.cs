using Unity.Netcode;
using UnityEngine;

public class IncreaseStat : Attack
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

        var player = transform.root.GetComponent<Player>();//.additiveAreaModifier += levelData.value;

        switch (levelData.stat)
        {
            case AuraBuffHolder.Stat.additiveMaxHealthModifier: player.additiveMaxHealthModifier += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageMaxHealthModifier: player.percentageMaxHealthModifier += levelData.value; break;
            case AuraBuffHolder.Stat.additiveDamageModifier: player.additiveDamageModifier += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageDamageModifier: player.percentageDamageModifier += levelData.value; break;
            case AuraBuffHolder.Stat.percentageCooldownModifier: player.percentageCooldownModifier += levelData.value; break;
            case AuraBuffHolder.Stat.additiveProjectileModifier: player.additiveProjectileModifier += (int)levelData.value; break;
            case AuraBuffHolder.Stat.additiveAreaModifier: player.additiveAreaModifier += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageAreaModifier: player.percentageAreaModifier += levelData.value; break;
            case AuraBuffHolder.Stat.enemySpawnModifier: player.enemySpawnModifier += levelData.value; break;
            case AuraBuffHolder.Stat.enemyDamageModifier: player.enemyDamageModifier += levelData.value; break;
            case AuraBuffHolder.Stat.playerHealthRegen: player.playerHealthRegen += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentagePlayerHealthRegen: player.percentagePlayerHealthRegen += levelData.value; break;
            case AuraBuffHolder.Stat.percentageTreasureFind: player.percentageTreasureFind += levelData.value; break;
            case AuraBuffHolder.Stat.percentageTreasurGain: player.percentageTreasurGain += levelData.value; break;
            case AuraBuffHolder.Stat.additivePlayerMoveSpeed: player.additivePlayerMoveSpeed += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentagePlayerMoveSpeed: player.percentagePlayerMoveSpeed += levelData.value; break;
            case AuraBuffHolder.Stat.additiveProjectileSpeed: player.additiveProjectileSpeed += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageProjectileSpeed: player.percentageProjectileSpeed += levelData.value; break;
            case AuraBuffHolder.Stat.additiveDuration: player.additiveDuration += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageDuration: player.percentageDuration += levelData.value; break;
            case AuraBuffHolder.Stat.additiveExperience: player.additiveExperience += (int)levelData.value; break;
            case AuraBuffHolder.Stat.percentageExperience: player.percentageExperience += levelData.value; break;


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }

        //level--;
    }
}
