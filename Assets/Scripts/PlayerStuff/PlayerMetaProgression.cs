using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaProgression : MonoBehaviour, IDataPersistence
{
    public static PlayerMetaProgression Instance;

    [SerializeField] public List<string> allAttacksPlayerUnlocked = new();

    public Dictionary<int,string> statNameList = new Dictionary<int, string>();

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

    [SerializeField] public SerializableDictionary<string, bool> skillTree;

    [SerializeField] public int maxSkillPoints;
    [SerializeField] public int spentSkillPoints;

    [SerializeField] public List<SeializableEnemyCard> enemyCardInventory;
    [SerializeField] public List<SeializableEnemyCard> enemyCardDeck;
    [SerializeField] public List<SerializableAttackCard> attackCardInventory;
    [SerializeField] public List<SerializableStatCard> statCardInventory;
    [SerializeField] public List<SerializableStatCard> statCardDeck;

    [SerializeField] public int maxAttackCards;
    [SerializeField] public int maxEnemyCards;
    [SerializeField] public int maxStatCards;

    [SerializeField] public SeializableEnemyCard testingCard;

    private void Awake()
    {
        Instance = this;
        statNameList.Add(0, "additiveMaxHealthModifier");
        statNameList.Add(1, "percentageMaxHealthModifier");
        statNameList.Add(2, "additiveDamageModifier");
        statNameList.Add(3, "percentageDamageModifier");
        statNameList.Add(4, "percentageCooldownModifier");
        statNameList.Add(5, "additiveProjectileModifier");
        statNameList.Add(6, "additiveAreaModifier");
        statNameList.Add(7, "percentageAreaModifier");
        statNameList.Add(8, "enemySpawnModifier");
        statNameList.Add(9, "enemyDamageModifier");
        statNameList.Add(10, "playerHealthRegen");
        statNameList.Add(11, "percentagePlayerHealthRegen");
        statNameList.Add(12, "percentageTreasureFind");
        statNameList.Add(13, "percentageTreasurGain");
        statNameList.Add(14, "additivePlayerMoveSpeed");
        statNameList.Add(15, "percentagePlayerMoveSpeed");
        statNameList.Add(16, "additiveProjectileSpeed");
        statNameList.Add(17, "percentageProjectileSpeed");
        statNameList.Add(18, "additiveDuration");
        statNameList.Add(19, "percentageDuration");
        statNameList.Add(20, "additiveExperience");
        statNameList.Add(21, "percentageExperience");
    }

    public bool AddAttack(string attackDataId)
    {
        if(allAttacksPlayerUnlocked.Contains(attackDataId))
            return false;
        allAttacksPlayerUnlocked.Add(attackDataId);
        return true;
    }

    public bool RemoveAttack(string attackDataId)
    {
        if(!allAttacksPlayerUnlocked.Contains(attackDataId))
            return false;
        allAttacksPlayerUnlocked.Remove(attackDataId);
        return true ;
    }

    public void ChangeStat(int statId, float Amount)
    {
        if (statId == 0) additiveMaxHealthModifier += (int)Amount;
        else if (statId == 1) percentageMaxHealthModifier += Amount;
        else if (statId == 2) additiveDamageModifier += (int)Amount;
        else if (statId == 3) percentageDamageModifier += Amount;
        else if (statId == 4) percentageCooldownModifier += Amount;
        else if (statId == 5) additiveProjectileModifier += (int)Amount;
        else if (statId == 6) additiveAreaModifier += (int)Amount;
        else if (statId == 7) percentageAreaModifier += Amount;
        else if (statId == 8) enemySpawnModifier += Amount;
        else if (statId == 9) enemyDamageModifier += Amount;
        else if (statId == 10) playerHealthRegen += (int)Amount;
        else if (statId == 11) percentagePlayerHealthRegen += Amount;
        else if (statId == 12) percentageTreasureFind += Amount;
        else if (statId == 13) percentageTreasurGain += Amount;
        else if (statId == 14) additivePlayerMoveSpeed += (int)Amount;
        else if (statId == 15) percentagePlayerMoveSpeed += Amount;
        else if (statId == 16) additiveProjectileSpeed += (int)Amount;
        else if (statId == 17) percentageProjectileSpeed += Amount;
        else if (statId == 18) additiveDuration += (int)Amount;
        else if (statId == 19) percentageDuration += Amount;
        else if (statId == 20) additiveExperience += (int)Amount;
        else if (statId == 21) percentageExperience += Amount;
        else if (statId == 100) { maxSkillPoints += (int)Amount; spentSkillPoints += (int)Amount; }
        else if (statId == 101) maxAttackCards += (int)Amount;
        else if (statId == 102) maxEnemyCards += (int)Amount;
        else if (statId == 103) maxStatCards += (int)Amount;
        //else if (statId == 104) maxSkillPoints += (int)Amount;
    }

    public string GetNameOfStat(int statId)
    {
        return statNameList[statId];
    }

    public float GetAmontOfStat(int statId)
    {
        if (statId == 0) return additiveMaxHealthModifier;
        else if (statId == 1) return percentageMaxHealthModifier;
        else if (statId == 2) return additiveDamageModifier;
        else if (statId == 3) return percentageDamageModifier;
        else if (statId == 4) return percentageCooldownModifier;
        else if (statId == 5) return additiveProjectileModifier;
        else if (statId == 6) return additiveAreaModifier;
        else if (statId == 7) return percentageAreaModifier;
        else if (statId == 8) return enemySpawnModifier;
        else if (statId == 9) return enemyDamageModifier;
        else if (statId == 10) return playerHealthRegen;
        else if (statId == 11) return percentagePlayerHealthRegen;
        else if (statId == 12) return percentageTreasureFind;
        else if (statId == 13) return percentageTreasurGain;
        else if (statId == 14) return additivePlayerMoveSpeed;
        else if (statId == 15) return percentagePlayerMoveSpeed;
        else if (statId == 16) return additiveProjectileSpeed;
        else if (statId == 17) return percentageProjectileSpeed;
        else if (statId == 18) return additiveDuration;
        else if (statId == 19) return percentageDuration;
        else if (statId == 20) return additiveExperience;
        else if (statId == 21) return percentageExperience;
        else if (statId == 100) return maxSkillPoints;
        //else if (statId == 100) spentSkillPoints += (int)Amount;
        else if (statId == 101) return maxAttackCards;
        else if (statId == 102) return maxEnemyCards;
        else if (statId == 103) return maxStatCards;
        else return 0;
    }

    public void ChangeCoinAmount(int amount)
    {
        coins += amount;
        DataPersistenceManager.Instance.SaveGame();
    }

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
        this.skillTree = data.skillTree;
        this.enemyCardInventory = data.enemyCardInventory;
        this.enemyCardDeck = data.enemyCardDeck;
        this.attackCardInventory = data.attackCardInventory;
        this.statCardInventory = data.statCardInventory;
        this.statCardDeck = data.statCardDeck;
        this.testingCard = data.testCard;
        this.maxSkillPoints = data.maxSkillPoints;
        this.spentSkillPoints = data.spentSkillPoints;
        this.maxAttackCards = data.maxAttackCards;
        this.maxEnemyCards = data.maxEnemyCards;
        this.maxStatCards = data.maxStatCards;
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
        data.skillTree = this.skillTree;
        data.enemyCardInventory = this.enemyCardInventory;
        data.enemyCardDeck = this.enemyCardDeck;
        data.attackCardInventory = this.attackCardInventory;
        data.statCardInventory = this.statCardInventory;
        data.statCardDeck = this.statCardDeck;
        data.testCard = this.testingCard;
        data.maxSkillPoints = this.maxSkillPoints;
        data.spentSkillPoints = this.spentSkillPoints;
        data.maxAttackCards = this.maxAttackCards;
        data.maxEnemyCards = this.maxEnemyCards;
        data.maxStatCards = this.maxStatCards;
    }

    public void AddEnemyCard(EnemyCard card, int whichPlace = 0)
    {
        SeializableEnemyCard seializableEnemyCard = new SeializableEnemyCard();

        seializableEnemyCard.cardName = card.cardName;
        seializableEnemyCard.cardText = card.cardText;
        seializableEnemyCard.cardId = card.cardId;
        seializableEnemyCard.cardBackgroundName = card.cardBackground.name;
        seializableEnemyCard.cardForegroundName = card.cardForeground.name;
        seializableEnemyCard.foilEffectName = card.foilEffect.name;
        seializableEnemyCard.isFoil = card.isFoil;
        seializableEnemyCard.amountOfPacks = card.amountOfPacks;
        seializableEnemyCard.packsSize = card.packsSize;
        seializableEnemyCard.typeOfEnemy = card.typeOfEnemy;

        if (whichPlace == 0)
        {
            enemyCardInventory.Add(seializableEnemyCard);
        }
        else if (whichPlace == 1)
        {
            if(enemyCardDeck.Exists(seializableEnemyCard => seializableEnemyCard.cardId == card.cardId)) { return; }
            enemyCardDeck.Add(seializableEnemyCard);
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public void RemoveEnemyCard(EnemyCard card, int whichPlace = 0)
    {
        SeializableEnemyCard seializableEnemyCard = new SeializableEnemyCard();

        seializableEnemyCard.cardName = card.cardName;
        seializableEnemyCard.cardText = card.cardText;
        seializableEnemyCard.cardId = card.cardId;
        seializableEnemyCard.cardBackgroundName = card.cardBackground.name;
        seializableEnemyCard.cardForegroundName = card.cardForeground.name;
        seializableEnemyCard.foilEffectName = card.foilEffect.name;
        seializableEnemyCard.isFoil = card.isFoil;
        seializableEnemyCard.amountOfPacks = card.amountOfPacks;
        seializableEnemyCard.packsSize = card.packsSize;
        seializableEnemyCard.typeOfEnemy = card.typeOfEnemy;

        if (whichPlace == 0)
        {
            enemyCardInventory.RemoveAll(seializableEnemyCard => seializableEnemyCard.cardId == card.cardId);
        }
        else if (whichPlace == 1)
        {
            Debug.Log("Removing Enemy Card from deck");
            //if(enemyCardDeck.Remove(seializableEnemyCard)) { Debug.Log("card Remved"); }

            enemyCardDeck.RemoveAll(seializableEnemyCard => seializableEnemyCard.cardId == card.cardId);
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public EnemyCard GetEnemyCard(int i, int whichPlace = 0)
    {
        EnemyCard enemyCard = ScriptableObject.CreateInstance<EnemyCard>();

        if (whichPlace == 0)
        {
            enemyCard.cardName = enemyCardInventory[i].cardName;
            enemyCard.cardText = enemyCardInventory[i].cardText;
            enemyCard.cardId = enemyCardInventory[i].cardId;
            enemyCard.cardBackground = Resources.Load<Sprite>("Sprites/" + enemyCardInventory[i].cardBackgroundName);
            enemyCard.cardForeground = Resources.Load<Sprite>("Sprites/" + enemyCardInventory[i].cardForegroundName);
            enemyCard.foilEffect = Resources.Load<Sprite>("Sprites/" + enemyCardInventory[i].foilEffectName);
            enemyCard.isFoil = enemyCardInventory[i].isFoil;
            enemyCard.amountOfPacks = enemyCardInventory[i].amountOfPacks;
            enemyCard.packsSize = enemyCardInventory[i].packsSize;
            enemyCard.typeOfEnemy = enemyCardInventory[i].typeOfEnemy;
        }
        else if (whichPlace == 1)
        {
            enemyCard.cardName = enemyCardDeck[i].cardName;
            enemyCard.cardText = enemyCardDeck[i].cardText;
            enemyCard.cardId = enemyCardDeck[i].cardId;
            enemyCard.cardBackground = Resources.Load<Sprite>("Sprites/" + enemyCardDeck[i].cardBackgroundName);
            enemyCard.cardForeground = Resources.Load<Sprite>("Sprites/" + enemyCardDeck[i].cardForegroundName);
            enemyCard.foilEffect = Resources.Load<Sprite>("Sprites/" + enemyCardDeck[i].foilEffectName);
            enemyCard.isFoil = enemyCardDeck[i].isFoil;
            enemyCard.amountOfPacks = enemyCardDeck[i].amountOfPacks;
            enemyCard.packsSize = enemyCardDeck[i].packsSize;
            enemyCard.typeOfEnemy = enemyCardDeck[i].typeOfEnemy;
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }

        return enemyCard;
    }

    public void AddStatCard(StatCard card, int whichPlace = 0)
    {
        SerializableStatCard serializableStatCard = new SerializableStatCard();

        serializableStatCard.cardName = card.cardName;
        serializableStatCard.cardText = card.cardText;
        serializableStatCard.cardId = card.cardId;
        serializableStatCard.cardBackgroundName = card.cardBackground.name;
        serializableStatCard.cardForegroundName = card.cardForeground.name;
        serializableStatCard.foilEffectName = card.foilEffect.name;
        serializableStatCard.isFoil = card.isFoil;
        serializableStatCard.statId = card.statId;
        serializableStatCard.statChangeAmount = card.statChangeAmount;

        if (whichPlace == 0)
        {
            statCardInventory.Add(serializableStatCard);
        }
        else if (whichPlace == 1)
        {
            if (statCardDeck.Exists(seializableStatCard => seializableStatCard.cardId == card.cardId)) { return; }
            statCardDeck.Add(serializableStatCard);
            StatOrderComp.Instance.StatComp();
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public void RemoveStatCard(StatCard card, int whichPlace = 0)
    {
        SerializableStatCard serializableStatCard = new SerializableStatCard();

        serializableStatCard.cardName = card.cardName;
        serializableStatCard.cardText = card.cardText;
        serializableStatCard.cardId = card.cardId;
        serializableStatCard.cardBackgroundName = card.cardBackground.name;
        serializableStatCard.cardForegroundName = card.cardForeground.name;
        serializableStatCard.foilEffectName = card.foilEffect.name;
        serializableStatCard.isFoil = card.isFoil;
        serializableStatCard.statId = card.statId;
        serializableStatCard.statChangeAmount = card.statChangeAmount;

        if (whichPlace == 0)
        {
            statCardInventory.RemoveAll(serializableStatCard => serializableStatCard.cardId == card.cardId);
        }
        else if (whichPlace == 1)
        {
            Debug.Log("removing Stat Card from deck");
            statCardDeck.RemoveAll(serializableStatCard => serializableStatCard.cardId == card.cardId);
            StatOrderComp.Instance.StatComp();
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public StatCard GetStatCard(int i, int whichPlace = 0)
    {
        StatCard statCard = ScriptableObject.CreateInstance<StatCard>();

        if (whichPlace == 0)
        {
            statCard.cardName = statCardInventory[i].cardName;
            statCard.cardText = statCardInventory[i].cardText;
            statCard.cardId = statCardInventory[i].cardId;
            statCard.cardBackground = Resources.Load<Sprite>("Sprites/" + statCardInventory[i].cardBackgroundName);
            statCard.cardForeground = Resources.Load<Sprite>("Sprites/" + statCardInventory[i].cardForegroundName); ;
            statCard.foilEffect = Resources.Load<Sprite>("Sprites/" + statCardInventory[i].foilEffectName);
            statCard.isFoil = statCardInventory[i].isFoil;
            statCard.statId = statCardInventory[i].statId;
            statCard.statChangeAmount = statCardInventory[i].statChangeAmount;
        }
        else if (whichPlace == 1)
        {
            statCard.cardName = statCardDeck[i].cardName;
            statCard.cardText = statCardDeck[i].cardText;
            statCard.cardId = statCardDeck[i].cardId;
            statCard.cardBackground = Resources.Load<Sprite>("Sprites/" + statCardDeck[i].cardBackgroundName);
            statCard.cardForeground = Resources.Load<Sprite>("Sprites/" + statCardDeck[i].cardForegroundName); ;
            statCard.foilEffect = Resources.Load<Sprite>("Sprites/" + statCardDeck[i].foilEffectName);
            statCard.isFoil = statCardDeck[i].isFoil;
            statCard.statId = statCardDeck[i].statId;
            statCard.statChangeAmount = statCardDeck[i].statChangeAmount;
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }


        return statCard;
    }

    public void AddAttackCard(AttackCard card, int whichPlace = 0)
    {
        SerializableAttackCard serializableAttackCard = new SerializableAttackCard();

        serializableAttackCard.cardName = card.cardName;
        serializableAttackCard.cardText = card.cardText;
        serializableAttackCard.cardId = card.cardId;
        serializableAttackCard.cardBackgroundName = card.cardBackground.name;
        serializableAttackCard.cardForegroundName = card.cardForeground.name;
        serializableAttackCard.foilEffectName = card.foilEffect.name;
        serializableAttackCard.isFoil = card.isFoil;
        serializableAttackCard.attackId = card.attackId;
        
        if (whichPlace == 0)
        {
            attackCardInventory.Add(serializableAttackCard);
        }
        else if (whichPlace == 1)
        {
            allAttacksPlayerUnlocked.Add(card.attackId);
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public void RemoveAttackCard(AttackCard card, int whichPlace = 0)
    {
        SerializableAttackCard serializableAttackCard = new SerializableAttackCard();

        serializableAttackCard.cardName = card.cardName;
        serializableAttackCard.cardText = card.cardText;
        serializableAttackCard.cardId = card.cardId;
        serializableAttackCard.cardBackgroundName = card.cardBackground.name;
        serializableAttackCard.cardForegroundName = card.cardForeground.name;
        serializableAttackCard.foilEffectName = card.foilEffect.name;
        serializableAttackCard.isFoil = card.isFoil;
        serializableAttackCard.attackId = card.attackId;

        if (whichPlace == 0)
        {
            attackCardInventory.Remove(serializableAttackCard);
        }
        else if (whichPlace == 1)
        {
            allAttacksPlayerUnlocked.Remove(card.attackId);
        }
        else
        {
            Debug.Log("No Inventory of the place");
        }
    }

    public AttackCard GetAttackCard(int i, int whichPlace = 0)
    {
        AttackCard attackCard = ScriptableObject.CreateInstance<AttackCard>();

        attackCard.cardName = attackCardInventory[i].cardName;
        attackCard.cardText = attackCardInventory[i].cardText;
        attackCard.cardId = attackCardInventory[i].cardId;
        attackCard.cardBackground = Resources.Load<Sprite>("Sprites/" + attackCardInventory[i].cardBackgroundName);
        attackCard.cardForeground = Resources.Load<Sprite>("Sprites/" + attackCardInventory[i].cardForegroundName);
        attackCard.foilEffect = Resources.Load<Sprite>("Sprites/" + attackCardInventory[i].foilEffectName);
        attackCard.isFoil = attackCardInventory[i].isFoil;

        attackCard.attackId = attackCardInventory[i].attackId;

        return attackCard;
    }
}
