using UnityEngine;

public class ConvertionSkillNode : AltSkillNodes
{
    [SerializeField] private PlayerBaseStats.Stat fromStat;
    [SerializeField] private PlayerBaseStats.Stat toStat;
    [SerializeField] private float convertionRate;
    [SerializeField] private float convertionFrom;
    [SerializeField] private float convertionTo;
    [SerializeField] private string description;

    public override void SkillUpgrade()
    {
        convertionRate = convertionTo / convertionFrom;

        PlayerMetaProgression.Instance.ChangeStat((int)toStat, PlayerMetaProgression.Instance.GetAmontOfStat((int)fromStat) * convertionRate);

        switch (fromStat)
        {
            case PlayerBaseStats.Stat.additiveMaxHealthModifier: PlayerMetaProgression.Instance.additiveMaxHealthModifier = 0; break;
            case PlayerBaseStats.Stat.percentageMaxHealthModifier: PlayerMetaProgression.Instance.percentageMaxHealthModifier = 1; break;
            case PlayerBaseStats.Stat.additiveDamageModifier: PlayerMetaProgression.Instance.additiveDamageModifier = 0; break;
            case PlayerBaseStats.Stat.percentageDamageModifier: PlayerMetaProgression.Instance.percentageDamageModifier = 1; break;
            case PlayerBaseStats.Stat.percentageCooldownModifier: PlayerMetaProgression.Instance.percentageCooldownModifier = 1; break;
            case PlayerBaseStats.Stat.additiveProjectileModifier: PlayerMetaProgression.Instance.additiveProjectileModifier = 0; break;
            case PlayerBaseStats.Stat.additiveAreaModifier: PlayerMetaProgression.Instance.additiveAreaModifier = 0; break;
            case PlayerBaseStats.Stat.percentageAreaModifier: PlayerMetaProgression.Instance.percentageAreaModifier = 1; break;
            case PlayerBaseStats.Stat.enemySpawnModifier: PlayerMetaProgression.Instance.enemySpawnModifier = 0; break;
            case PlayerBaseStats.Stat.enemyDamageModifier: PlayerMetaProgression.Instance.enemyDamageModifier = 0; break;
            case PlayerBaseStats.Stat.playerHealthRegen: PlayerMetaProgression.Instance.playerHealthRegen = 0; break;
            case PlayerBaseStats.Stat.percentagePlayerHealthRegen: PlayerMetaProgression.Instance.percentagePlayerHealthRegen = 1; break;
            case PlayerBaseStats.Stat.percentageTreasureFind: PlayerMetaProgression.Instance.percentageTreasureFind = 1; break;
            case PlayerBaseStats.Stat.percentageTreasurGain: PlayerMetaProgression.Instance.percentageTreasurGain = 1; break;
            case PlayerBaseStats.Stat.additivePlayerMoveSpeed: PlayerMetaProgression.Instance.additivePlayerMoveSpeed = 0; break;
            case PlayerBaseStats.Stat.percentagePlayerMoveSpeed: PlayerMetaProgression.Instance.percentagePlayerMoveSpeed = 1; break;
            case PlayerBaseStats.Stat.additiveProjectileSpeed: PlayerMetaProgression.Instance.additiveProjectileSpeed = 0; break;
            case PlayerBaseStats.Stat.percentageProjectileSpeed: PlayerMetaProgression.Instance.percentageProjectileSpeed = 1; break;
            case PlayerBaseStats.Stat.additiveDuration: PlayerMetaProgression.Instance.additiveDuration = 0; break;
            case PlayerBaseStats.Stat.percentageDuration: PlayerMetaProgression.Instance.percentageDuration = 1; break;
            case PlayerBaseStats.Stat.additiveExperience: PlayerMetaProgression.Instance.additiveExperience = 0; break;
            case PlayerBaseStats.Stat.percentageExperience: PlayerMetaProgression.Instance.percentageExperience = 1; break;


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }
    }

    public override string GetDescription()
    {
        description = "Converts " + PlayerMetaProgression.Instance.GetNameOfStat((int)fromStat) + " to " + PlayerMetaProgression.Instance.GetNameOfStat((int)toStat) +
            " at a ratio of " + convertionFrom + " to " + convertionTo;

        return description;
    }
}
