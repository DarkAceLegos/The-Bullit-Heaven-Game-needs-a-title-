using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaProgression : MonoBehaviour, IDataPersistence
{
    public static PlayerMetaProgression Instance;

    [SerializeField] public List<AttackData> allAttacksPlayerUnlocked = new();

    private Dictionary<int,string> statList = new Dictionary<int, string>();

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
        statList.Add(0, "additiveMaxHealthModifier");
        statList.Add(1, "percentageMaxHealthModifier");
        statList.Add(2, "additiveDamageModifier");
        statList.Add(3, "percentageDamageModifier");
        statList.Add(4, "percentageCooldownModifier");
        statList.Add(5, "additiveProjectileModifier");
        statList.Add(6, "additiveAreaModifier");
        statList.Add(7, "percentageAreaModifier");
        statList.Add(8, "enemySpawnModifier");
        statList.Add(9, "enemyDamageModifier");
        statList.Add(10, "playerHealthRegen");
        statList.Add(11, "percentagePlayerHealthRegen");
        statList.Add(12, "percentageTreasureFind");
        statList.Add(13, "percentageTreasurGain");
        statList.Add(14, "additivePlayerMoveSpeed");
        statList.Add(15, "percentagePlayerMoveSpeed");
        statList.Add(16, "additiveProjectileSpeed");
        statList.Add(17, "percentageProjectileSpeed");
        statList.Add(18, "additiveDuration");
        statList.Add(19, "percentageDuration");
        statList.Add(20, "additiveExperience");
        statList.Add(21, "percentageExperience");
    }

    public void AddAttack(AttackData attackData)
    {
        if(allAttacksPlayerUnlocked.Contains(attackData))
            return;
        allAttacksPlayerUnlocked.Add(attackData);
    }

    public void RemoveAttack(AttackData attackData)
    {
        if(!allAttacksPlayerUnlocked.Contains(attackData))
            return ;
        allAttacksPlayerUnlocked.Remove(attackData);
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
        return statList[statId];
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
