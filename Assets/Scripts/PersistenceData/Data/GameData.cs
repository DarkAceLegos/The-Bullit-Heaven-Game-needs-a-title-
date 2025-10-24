using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<AttackData> allAttacksPlayerUnlocked;
    public int additiveMaxHealthModifier;
    public float percentageMaxHealthModifier = 1f;
    public int additiveDamageModifier;
    public float percentageDamageModifier = 1f;
    public float percentageCooldownModifier = 1f;
    public int additiveProjectileModifier;
    public int additiveAreaModifier;
    public float percentageAreaModifier = 1f;
    public float enemySpawnModifier;
    public float enemyDamageModifier;
    public int playerHealthRegen;
    public float percentagePlayerHealthRegen = 1f;
    public float percentageTreasureFind = 1f;
    public float percentageTreasurGain = 1f;
    public int additivePlayerMoveSpeed;
    public float percentagePlayerMoveSpeed = 1f;
    public int additiveProjectileSpeed;
    public float percentageProjectileSpeed = 1f;
    public int additiveDuration;
    public float percentageDuration = 1f;
    public int additiveExperience;
    public float percentageExperience = 1f;


     public int coins;
     public int gems;

    public GameData()
    {
        this.allAttacksPlayerUnlocked = new List<AttackData>();
        this.additiveMaxHealthModifier = 0;
        this.percentageMaxHealthModifier = 1f;
        this.additiveDamageModifier = 0;
        this.percentageDamageModifier = 1f;
        this.percentageCooldownModifier = 1f;
        this.additiveProjectileModifier = 0;
        this.additiveAreaModifier = 0;
        this.percentageAreaModifier = 1f;
        this.enemySpawnModifier = 1f;
        this.enemyDamageModifier = 1f;
        this.playerHealthRegen = 0;
        this.percentagePlayerHealthRegen = 1f;
        this.percentageTreasureFind = 1f;
        this.percentageTreasurGain = 1f;
        this.additivePlayerMoveSpeed = 0;
        this.percentagePlayerMoveSpeed = 1f;
        this.additiveProjectileSpeed = 0;
        this.percentageProjectileSpeed = 1f;
        this.additiveDuration = 0;
        this.percentageDuration = 1f;
        this.additiveExperience = 0;
        this.percentageExperience = 1f;
        this.coins = 0;
        this.gems = 0;
    }
}
