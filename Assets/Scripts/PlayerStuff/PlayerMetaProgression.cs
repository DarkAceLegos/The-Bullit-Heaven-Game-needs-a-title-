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

    public void LoadData(PlayerMetaProgression progression)
    {
        this.additiveAreaModifier = progression.additiveAreaModifier;
    }

    public void SaveData(ref PlayerMetaProgression progression)
    {
        progression.additiveAreaModifier = this.additiveAreaModifier;
    }

    public void SetPlayerMetaProgressionPercentDamageModifier(float percentageDamageModifier1)
    {
        percentageDamageModifier = percentageDamageModifier1;
    }
}
