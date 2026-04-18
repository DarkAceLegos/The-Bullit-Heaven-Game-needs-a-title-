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


    public void AuraBuff(Stat stat, float amount)
    {
        switch (stat)
        {
            case Stat.additiveMaxHealthModifier: player.additiveMaxHealthModifier += (int)amount; break;
            case Stat.percentageMaxHealthModifier: player.percentageMaxHealthModifier += amount; break;
            case Stat.additiveDamageModifier: player.additiveDamageModifier += (int)amount; break;
            case Stat.percentageDamageModifier: player.percentageDamageModifier += amount; break;
            case Stat.percentageCooldownModifier: player.percentageCooldownModifier += amount; break;
            case Stat.additiveProjectileModifier: player.additiveProjectileModifier += (int)amount; break;
            case Stat.additiveAreaModifier: player.additiveAreaModifier += (int)amount; break;
            case Stat.percentageAreaModifier: player.percentageAreaModifier += amount; break;
            case Stat.enemySpawnModifier: player.enemySpawnModifier += amount; break;
            case Stat.enemyDamageModifier: player.enemyDamageModifier += amount; break;
            case Stat.playerHealthRegen: player.playerHealthRegen += (int)amount; break;
            case Stat.percentagePlayerHealthRegen: player.percentagePlayerHealthRegen += amount; break;
            case Stat.percentageTreasureFind: player.percentageTreasureFind += amount; break;
            case Stat.percentageTreasurGain: player.percentageTreasurGain += amount; break;
            case Stat.additivePlayerMoveSpeed: player.additivePlayerMoveSpeed += (int)amount; break;
            case Stat.percentagePlayerMoveSpeed: player.percentagePlayerMoveSpeed += amount; break;
            case Stat.additiveProjectileSpeed: player.additiveProjectileSpeed += (int)amount; break;
            case Stat.percentageProjectileSpeed: player.percentageProjectileSpeed += amount; break;
            case Stat.additiveDuration: player.additiveDuration += (int)amount; break;
            case Stat.percentageDuration: player.percentageDuration += amount; break;
            case Stat.additiveExperience: player.additiveExperience += (int)amount; break;
            case Stat.percentageExperience: player.percentageExperience += amount; break;


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }
    }
}
