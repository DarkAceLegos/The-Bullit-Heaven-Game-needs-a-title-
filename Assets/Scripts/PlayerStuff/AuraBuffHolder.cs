using UnityEngine;

public class AuraBuffHolder : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public enum Stat
    {
        additiveMaxHealthModifier,
        percentageMaxHealthModifier,
        additiveDamageModifier,
        percentageDamageModifier,
        percentageCooldownModifier,
        additiveProjectileModifier,
        additiveAreaModifier,
        percentageAreaModifier,
        enemySpawnModifier,
        enemyDamageModifier,
        playerHealthRegen,
        percentagePlayerHealthRegen,
        percentageTreasureFind,
        percentageTreasurGain,
        additivePlayerMoveSpeed,
        percentagePlayerMoveSpeed,
        additiveProjectileSpeed,
        percentageProjectileSpeed,
        additiveDuration,
        percentageDuration,
        additiveExperience,
        percentageExperience,
    }


    public void AuraBuff(PlayerBaseStats.Stat stat, float amount)
    {
        switch (stat)
        {
            case PlayerBaseStats.Stat.additiveMaxHealthModifier: player.additiveMaxHealthModifier += (int)amount; break;
            case PlayerBaseStats.Stat.percentageMaxHealthModifier: player.percentageMaxHealthModifier += amount; break;
            case PlayerBaseStats.Stat.additiveDamageModifier: player.additiveDamageModifier += (int)amount; break;
            case PlayerBaseStats.Stat.percentageDamageModifier: player.percentageDamageModifier += amount; break;
            case PlayerBaseStats.Stat.percentageCooldownModifier: player.percentageCooldownModifier += amount; break;
            case PlayerBaseStats.Stat.additiveProjectileModifier: player.additiveProjectileModifier += (int)amount; break;
            case PlayerBaseStats.Stat.additiveAreaModifier: player.additiveAreaModifier += (int)amount; break;
            case PlayerBaseStats.Stat.percentageAreaModifier: player.percentageAreaModifier += amount; break;
            case PlayerBaseStats.Stat.enemySpawnModifier: player.enemySpawnModifier += amount; break;
            case PlayerBaseStats.Stat.enemyDamageModifier: player.enemyDamageModifier += amount; break;
            case PlayerBaseStats.Stat.playerHealthRegen: player.playerHealthRegen += (int)amount; break;
            case PlayerBaseStats.Stat.percentagePlayerHealthRegen: player.percentagePlayerHealthRegen += amount; break;
            case PlayerBaseStats.Stat.percentageTreasureFind: player.percentageTreasureFind += amount; break;
            case PlayerBaseStats.Stat.percentageTreasurGain: player.percentageTreasurGain += amount; break;
            case PlayerBaseStats.Stat.additivePlayerMoveSpeed: player.additivePlayerMoveSpeed += (int)amount; break;
            case PlayerBaseStats.Stat.percentagePlayerMoveSpeed: player.percentagePlayerMoveSpeed += amount; break;
            case PlayerBaseStats.Stat.additiveProjectileSpeed: player.additiveProjectileSpeed += (int)amount; break;
            case PlayerBaseStats.Stat.percentageProjectileSpeed: player.percentageProjectileSpeed += amount; break;
            case PlayerBaseStats.Stat.additiveDuration: player.additiveDuration += (int)amount; break;
            case PlayerBaseStats.Stat.percentageDuration: player.percentageDuration += amount; break;
            case PlayerBaseStats.Stat.additiveExperience: player.additiveExperience += (int)amount; break;
            case PlayerBaseStats.Stat.percentageExperience: player.percentageExperience += amount; break;


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }
    }
}
