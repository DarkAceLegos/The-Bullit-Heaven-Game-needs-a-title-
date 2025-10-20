using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaProgression : MonoBehaviour
{
    [SerializeField] private List<AttackData> allAttacksPlayerUnlocked = new(); //
    [SerializeField] private int additiveMaxHealthModifier;
    [SerializeField] private float percentageMaxHealthModifier;
    [SerializeField] private int additiveDamageModifier;
    [SerializeField] private float percentageDamageModifier;
    [SerializeField] private float percentageCooldownModifier;
    [SerializeField] private int additiveProjectileModifier;
    [SerializeField] private int additiveAreaModifier;
    [SerializeField] private float percentageAreaModifier;
    [SerializeField] private float enemySpawnModifier;
    [SerializeField] private float enemyDamageModifier;
    [SerializeField] private int playerHealthRegen;
    [SerializeField] private float percentagePLayerHealthRegen;
    [SerializeField] private float percentageTreasureFind;
    [SerializeField] private float percentageTreasurGain;


    [SerializeField] private int coins;
    [SerializeField] private int gems;
}
