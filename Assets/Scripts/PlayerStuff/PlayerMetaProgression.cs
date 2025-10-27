using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaProgression : MonoBehaviour, IDataPersistence
{
    public static PlayerMetaProgression Instance;

    [SerializeField] public List<AttackData> allAttacksPlayerUnlocked = new();

    private Dictionary<int,string> statNameList = new Dictionary<int, string>();

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

    public bool AddAttack(AttackData attackData)
    {
        if(allAttacksPlayerUnlocked.Contains(attackData))
            return false;
        allAttacksPlayerUnlocked.Add(attackData);
        return true;
    }

    public bool RemoveAttack(AttackData attackData)
    {
        if(!allAttacksPlayerUnlocked.Contains(attackData))
            return false;
        allAttacksPlayerUnlocked.Remove(attackData);
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
