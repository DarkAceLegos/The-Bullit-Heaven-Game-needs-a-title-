using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatOrderComp : MonoBehaviour
{
    [SerializeField] public PlayerBaseStats player; 

    public void StatComp()
    {
        ResetStats(player);

        List<SkillNode> listOrder0 = new List<SkillNode>();
        List<SkillNode> listOrder1 = new List<SkillNode>();
        List<SkillNode> listOrder2 = new List<SkillNode>();

        foreach (var card in PlayerMetaProgression.Instance.statCardDeck)
        {
            for (int i = 0; i > card.statId.Count; i++)
            {
                PlayerMetaProgression.Instance.ChangeStat(card.statId[i], card.statChangeAmount[i]);
            } 
        }

        foreach (var skill in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<SkillNode>())
        {
            if(skill.unlocked)
            {
                if (skill.order == 0)
                {
                    listOrder0.Add(skill);
                }
                else if (skill.order == 1)
                {
                    listOrder1.Add(skill);
                }
                else if(skill.order == 2)
                {
                    listOrder2.Add(skill);
                }
            }
        }
        if(listOrder0.Count > 0)
        {
            foreach (var skill in listOrder0)
            {
                skill.StatSkillNodeComp();
            }
        }
        if (listOrder1.Count > 0)
        {
            foreach (var skill in listOrder1)
            {
                skill.StatSkillNodeComp();
            }
        }
        if (listOrder2.Count > 0)
        {
            foreach (var skill in listOrder2)
            {
                skill.StatSkillNodeComp();
            }
        }
    }

    private void ResetStats(PlayerBaseStats player)
    {
        PlayerMetaProgression.Instance.additiveMaxHealthModifier = player.additiveMaxHealthModifier;
        PlayerMetaProgression.Instance.percentageMaxHealthModifier = player.percentageMaxHealthModifier;
        PlayerMetaProgression.Instance.additiveDamageModifier = player.additiveDamageModifier;
        PlayerMetaProgression.Instance.percentageDamageModifier = player.percentageDamageModifier;
        PlayerMetaProgression.Instance.percentageCooldownModifier = player.percentageCooldownModifier;
        PlayerMetaProgression.Instance.additiveProjectileModifier = player.additiveProjectileModifier;
        PlayerMetaProgression.Instance.additiveAreaModifier = player.additiveAreaModifier;
        PlayerMetaProgression.Instance.percentageAreaModifier = player.percentageAreaModifier;
        PlayerMetaProgression.Instance.enemySpawnModifier = player.enemySpawnModifier;
        PlayerMetaProgression.Instance.enemyDamageModifier = player.enemyDamageModifier;
        PlayerMetaProgression.Instance.playerHealthRegen = player.playerHealthRegen;
        PlayerMetaProgression.Instance.percentagePlayerHealthRegen = player.percentagePlayerHealthRegen;
        PlayerMetaProgression.Instance.percentageTreasureFind = player.percentageTreasureFind;
        PlayerMetaProgression.Instance.percentageTreasurGain = player.percentageTreasurGain;
        PlayerMetaProgression.Instance.additivePlayerMoveSpeed = player.additivePlayerMoveSpeed;
        PlayerMetaProgression.Instance.percentagePlayerMoveSpeed = player.percentagePlayerMoveSpeed;
        PlayerMetaProgression.Instance.additiveProjectileSpeed = player.additiveProjectileSpeed;
        PlayerMetaProgression.Instance.percentageProjectileSpeed = player.percentageProjectileSpeed;
        PlayerMetaProgression.Instance.additiveDuration = player.additiveDuration;
        PlayerMetaProgression.Instance.percentageDuration = player.percentageDuration;
        PlayerMetaProgression.Instance.additiveExperience = player.additiveExperience;
        PlayerMetaProgression.Instance.percentageExperience = player.percentageExperience;
    }
}
