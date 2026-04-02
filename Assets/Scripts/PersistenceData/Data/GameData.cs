using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public List<string> allAttacksPlayerUnlocked;
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

    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, bool> unlocks;

    public int maxSkillPoints;
    public int spentSkillPoints;

    public List<SeializableEnemyCard> enemyCardInventory;
    public List<SeializableEnemyCard> enemyCardDeck;
    public List<SerializableAttackCard> attackCardInventory;
    public List<SerializableStatCard> statCardInventory;
    public List<SerializableStatCard> statCardDeck;

    public int maxAttackCards;
    public int maxEnemyCards;
    public int maxStatCards;

    public SeializableEnemyCard testCard;

    public int coins;
    public int gems;

    public GameData()
    {
        this.allAttacksPlayerUnlocked = new List<string> {};
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

        skillTree = new SerializableDictionary<string, bool>();

        this.maxSkillPoints = 0;
        this.spentSkillPoints = 0;

        unlocks = new SerializableDictionary<string, bool>();

        enemyCardInventory = new List<SeializableEnemyCard>();
        enemyCardDeck = new List<SeializableEnemyCard>();
        attackCardInventory = new List<SerializableAttackCard>();
        statCardInventory = new List<SerializableStatCard>();
        statCardDeck = new List<SerializableStatCard>();

        this.maxAttackCards = 5;
        this.maxStatCards = 1;
        this.maxEnemyCards = 30;

        testCard = new SeializableEnemyCard();

        this.coins = 10;
        this.gems = 0;
    }
}
