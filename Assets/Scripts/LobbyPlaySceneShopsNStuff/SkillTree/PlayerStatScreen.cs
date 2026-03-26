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

        statText.text = $"additiveMaxHealthModifier {playerMetaProgression.additiveMaxHealthModifier} \n\n" +
            $"percentageMaxHealthModifier {playerMetaProgression.percentageMaxHealthModifier}\n\n" +
            $"additiveDamageModifier {playerMetaProgression.additiveDamageModifier}\n\n" +
            $"percentageDamageModifier {playerMetaProgression.percentageDamageModifier}\n\n" +
            $"percentageCooldownModifier {playerMetaProgression.percentageCooldownModifier}\n\n" +
            $"additiveProjectileModifier {playerMetaProgression.additiveProjectileModifier}\n\n" +
            $"additiveAreaModifier {playerMetaProgression.additiveAreaModifier}\n\n" +
            $"percentageAreaModifier {playerMetaProgression.percentageAreaModifier}\n\n" +
            $"enemySpawnModifier {playerMetaProgression.enemySpawnModifier}\n\n" +
            $"enemyDamageModifier {playerMetaProgression.enemyDamageModifier}\n\n" +
            $"playerHealthRegen {playerMetaProgression.playerHealthRegen}\n\n" +
            $"percentagePlayerHealthRegen {playerMetaProgression.percentagePlayerHealthRegen}\n\n" +
            $"percentageTreasureFind {playerMetaProgression.percentageTreasureFind}\n\n" +
            $"percentageTreasurGain {playerMetaProgression.percentageTreasurGain}\n\n" + 
            $"additivePlayerMoveSpeed {playerMetaProgression.additivePlayerMoveSpeed}\n\n" +
            $"percentagePlayerMoveSpeed {playerMetaProgression.percentagePlayerMoveSpeed}\n\n" +
            $"additiveProjectileSpeed {playerMetaProgression.additiveProjectileSpeed}\n\n" +
            $"percentageProjectileSpeed {playerMetaProgression.percentageProjectileSpeed}\n\n" +
            $"additiveDuration {playerMetaProgression.additiveDuration}\n\n" +
            $"percentageDuration {playerMetaProgression.percentageDuration}\n\n" +
            $"additiveExperience {playerMetaProgression.additiveExperience}\n\n" +
            $"percentageExperience {playerMetaProgression.percentageExperience}\n\n";
    }
}
