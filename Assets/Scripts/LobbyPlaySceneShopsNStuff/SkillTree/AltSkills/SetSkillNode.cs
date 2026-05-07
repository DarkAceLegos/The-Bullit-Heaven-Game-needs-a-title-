using UnityEngine;

public class SetSkillNode : AltSkillNodes
{
    [SerializeField] private PlayerBaseStats.Stat stat;
    [SerializeField] private float amount;
    [SerializeField] private string description;

    public override string GetDescription()
    {
        //throw new System.NotImplementedException();

        return description;
    }

    public override void SkillUpgrade()
    {
        switch (stat)
        {
            case PlayerBaseStats.Stat.additiveMaxHealthModifier: PlayerMetaProgression.Instance.additiveMaxHealthModifier = (int)amount; break;
            case PlayerBaseStats.Stat.percentageMaxHealthModifier: PlayerMetaProgression.Instance.percentageMaxHealthModifier = amount; break;
            case PlayerBaseStats.Stat.additiveDamageModifier: PlayerMetaProgression.Instance.additiveDamageModifier = (int)amount; break;
            case PlayerBaseStats.Stat.percentageDamageModifier: PlayerMetaProgression.Instance.percentageDamageModifier = amount; break;
            case PlayerBaseStats.Stat.percentageCooldownModifier: PlayerMetaProgression.Instance.percentageCooldownModifier = amount; break;
            case PlayerBaseStats.Stat.additiveProjectileModifier: PlayerMetaProgression.Instance.additiveProjectileModifier = (int)amount; break;
            case PlayerBaseStats.Stat.additiveAreaModifier: PlayerMetaProgression.Instance.additiveAreaModifier = (int)amount; break;
            case PlayerBaseStats.Stat.percentageAreaModifier: PlayerMetaProgression.Instance.percentageAreaModifier = amount; break;
            case PlayerBaseStats.Stat.enemySpawnModifier: PlayerMetaProgression.Instance.enemySpawnModifier = amount; break;
            case PlayerBaseStats.Stat.enemyDamageModifier: PlayerMetaProgression.Instance.enemyDamageModifier = amount; break;
            case PlayerBaseStats.Stat.playerHealthRegen: PlayerMetaProgression.Instance.playerHealthRegen = (int)amount; break;
            case PlayerBaseStats.Stat.percentagePlayerHealthRegen: PlayerMetaProgression.Instance.percentagePlayerHealthRegen = amount; break;
            case PlayerBaseStats.Stat.percentageTreasureFind: PlayerMetaProgression.Instance.percentageTreasureFind = amount; break;
            case PlayerBaseStats.Stat.percentageTreasurGain: PlayerMetaProgression.Instance.percentageTreasurGain = amount; break;
            case PlayerBaseStats.Stat.additivePlayerMoveSpeed: PlayerMetaProgression.Instance.additivePlayerMoveSpeed = (int)amount; break;
            case PlayerBaseStats.Stat.percentagePlayerMoveSpeed: PlayerMetaProgression.Instance.percentagePlayerMoveSpeed = amount; break;
            case PlayerBaseStats.Stat.additiveProjectileSpeed: PlayerMetaProgression.Instance.additiveProjectileSpeed = (int)amount; break;
            case PlayerBaseStats.Stat.percentageProjectileSpeed: PlayerMetaProgression.Instance.percentageProjectileSpeed = amount; break;
            case PlayerBaseStats.Stat.additiveDuration: PlayerMetaProgression.Instance.additiveDuration = (int)amount; break;
            case PlayerBaseStats.Stat.percentageDuration: PlayerMetaProgression.Instance.percentageDuration = amount; break;
            case PlayerBaseStats.Stat.additiveExperience: PlayerMetaProgression.Instance.additiveExperience = (int)amount; break;
            case PlayerBaseStats.Stat.percentageExperience: PlayerMetaProgression.Instance.percentageExperience = amount; break;


            default:
                Debug.Log("No Stat Chosesn");
                break;
        }
    }
}
