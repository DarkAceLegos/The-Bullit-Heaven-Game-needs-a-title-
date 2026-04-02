using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatScreen : MonoBehaviour
{
    public static PlayerStatScreen Instance;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private PlayerMetaProgression playerMetaProgression;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        UpdateViuals();
    }

    public void UpdateViuals()
    {
        StatOrderComp.Instance.StatComp();

        statText.text = $"additiveMaxHealthModifier {playerMetaProgression.additiveMaxHealthModifier} \n" +
            $"percentageMaxHealthModifier {playerMetaProgression.percentageMaxHealthModifier}\n" +
            $"additiveDamageModifier {playerMetaProgression.additiveDamageModifier}\n" +
            $"percentageDamageModifier {playerMetaProgression.percentageDamageModifier}\n" +
            $"percentageCooldownModifier {playerMetaProgression.percentageCooldownModifier}\n" +
            $"additiveProjectileModifier {playerMetaProgression.additiveProjectileModifier}\n" +
            $"additiveAreaModifier {playerMetaProgression.additiveAreaModifier}\n" +
            $"percentageAreaModifier {playerMetaProgression.percentageAreaModifier}\n" +
            $"enemySpawnModifier {playerMetaProgression.enemySpawnModifier}\n" +
            $"enemyDamageModifier {playerMetaProgression.enemyDamageModifier}\n" +
            $"playerHealthRegen {playerMetaProgression.playerHealthRegen}\n" +
            $"percentagePlayerHealthRegen {playerMetaProgression.percentagePlayerHealthRegen}\n" +
            $"percentageTreasureFind {playerMetaProgression.percentageTreasureFind}\n" +
            $"percentageTreasurGain {playerMetaProgression.percentageTreasurGain}\n" + 
            $"additivePlayerMoveSpeed {playerMetaProgression.additivePlayerMoveSpeed}\n" +
            $"percentagePlayerMoveSpeed {playerMetaProgression.percentagePlayerMoveSpeed}\n" +
            $"additiveProjectileSpeed {playerMetaProgression.additiveProjectileSpeed}\n" +
            $"percentageProjectileSpeed {playerMetaProgression.percentageProjectileSpeed}\n" +
            $"additiveDuration {playerMetaProgression.additiveDuration}\n" +
            $"percentageDuration {playerMetaProgression.percentageDuration}\n" +
            $"additiveExperience {playerMetaProgression.additiveExperience}\n" +
            $"percentageExperience {playerMetaProgression.percentageExperience}\n";
    }
}
