using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaProgression : MonoBehaviour, IDataPersistence
{
    [SerializeField] public List<AttackData> allAttacksPlayerUnlocked = new(); //
    [SerializeField] public int additiveMaxHealthModifier;
    [SerializeField] public float percentageMaxHealthModifier = 1f;
    [SerializeField] public int additiveDamageModifier;
    [SerializeField] public float percentageDamageModifier = 1f;
    [SerializeField] public float percentageCooldownModifier = 1f;
    [SerializeField] public int additiveProjectileModifier;
    [SerializeField] public int additiveAreaModifier;
    [SerializeField] public float percentageAreaModifier = 1f;
    [SerializeField] public float enemySpawnModifier;
    [SerializeField] public float enemyDamageModifier;
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


    [SerializeField] public int coins;
    [SerializeField] public int gems;

    public void LoadData(GameData data)
    {
        this.allAttacksPlayerUnlocked = data.allAttacksPlayerUnlocked;
        this.additiveMaxHealthModifier = data.additiveMaxHealthModifier;
        this.percentageMaxHealthModifier = data.percentageMaxHealthModifier;
        this.additiveDamageModifier = data.additiveDamageModifier;
        this.percentageDamageModifier = data.percentageDamageModifier;
        this.percentageCooldownModifier = data.percentageCooldownModifier;
        this.additiveProjectileModifier = data.additiveProjectileModifier;
        this.additiveAreaModifier = data.additiveAreaModifier;
        this.percentageAreaModifier = data.percentageAreaModifier;
        this.enemySpawnModifier = data.enemySpawnModifier;
        this.enemyDamageModifier = data.enemyDamageModifier;
        this.playerHealthRegen = data.playerHealthRegen;
        this.percentagePlayerHealthRegen = data.percentagePlayerHealthRegen;
        this.percentageTreasureFind = data.percentageTreasureFind;
        this.percentageTreasurGain = data.percentageTreasurGain;
        this.additivePlayerMoveSpeed = data.additivePlayerMoveSpeed;
        this.percentagePlayerMoveSpeed = data.percentagePlayerMoveSpeed;
        this.additiveProjectileSpeed = data.additiveProjectileSpeed;
        this.percentageProjectileSpeed = data.percentageProjectileSpeed;
        this.additiveDuration = data.additiveDuration;
        this.percentageDuration = data.percentageDuration;
        this.additiveExperience = data.additiveExperience;
        this.percentageExperience = data.percentageExperience;
        this.coins = data.coins;
        this.gems = data.gems;
    }

    public void SaveData(ref GameData data)
    {
        data.allAttacksPlayerUnlocked = this.allAttacksPlayerUnlocked;
        data.additiveMaxHealthModifier = this.additiveMaxHealthModifier;
        data.percentageMaxHealthModifier = this.percentageMaxHealthModifier;
        data.additiveDamageModifier = this.additiveDamageModifier;
        data.percentageDamageModifier = this.percentageDamageModifier;
        data.percentageCooldownModifier = this.percentageCooldownModifier;
        data.additiveProjectileModifier = this.additiveProjectileModifier;
        data.additiveAreaModifier = this.additiveAreaModifier;
        data.percentageAreaModifier = this.percentageAreaModifier;
        data.enemySpawnModifier = this.enemySpawnModifier;
        data.enemyDamageModifier = this.enemyDamageModifier;
        data.playerHealthRegen = this.playerHealthRegen;
        data.percentagePlayerHealthRegen = this.percentagePlayerHealthRegen;
        data.percentageTreasureFind = this.percentageTreasureFind;
        data.percentageTreasurGain = this.percentageTreasurGain;
        data.additivePlayerMoveSpeed = this.additivePlayerMoveSpeed;
        data.percentagePlayerMoveSpeed = this.percentagePlayerMoveSpeed;
        data.additiveProjectileSpeed = this.additiveProjectileSpeed;
        data.percentageProjectileSpeed = this.percentageProjectileSpeed;
        data.additiveDuration = this.additiveDuration;
        data.percentageDuration = this.percentageDuration;
        data.additiveExperience = this.additiveExperience;
        data.percentageExperience = this.percentageExperience;
        data.coins = this.coins;
        data.gems = this.gems;
    }


}
