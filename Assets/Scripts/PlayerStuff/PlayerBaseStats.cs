using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    [SerializeField] public int additiveMaxHealthModifier;
    [SerializeField] public float percentageMaxHealthModifier = 1f;
    [SerializeField] public int additiveDamageModifier;
    [SerializeField] public float percentageDamageModifier = 1f;
    [SerializeField] public float percentageCooldownModifier = 1f;
    [SerializeField] public int additiveProjectileModifier;
    [SerializeField] public int additiveAreaModifier;
    [SerializeField] public float percentageAreaModifier = 1f;
    [SerializeField] public float enemySpawnModifier = 1f;
    [SerializeField] public float enemyDamageModifier = 1f;
    [SerializeField] public int playerHealthRegen;
    [SerializeField] public float percentagePlayerHealthRegen = 1f;
    [SerializeField] public float percentageTreasureFind = 1f;
    [SerializeField] public float percentageTreasurGain = 1f;
    [SerializeField] public int additivePlayerMoveSpeed;
    [SerializeField] public float percentagePlayerMoveSpeed = 1f;
    [SerializeField] public int additiveProjectileSpeed;
    [SerializeField] public float percentageProjectileSpeed = 1f;
    [SerializeField] public int additiveDuration;
    [SerializeField] public float percentageDuration = 1f;
    [SerializeField] public int additiveExperience;
    [SerializeField] public float percentageExperience = 1f;

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
}
