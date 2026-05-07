using UnityEngine;

public class AddingAuraSkillNode : AltSkillNodes
{
    [SerializeField] private string description;
    [SerializeField] private AttackCard auraCard;

    public override string GetDescription()
    {
        return description;
    }

    public override void SkillUpgrade()
    {
        PlayerMetaProgression.Instance.AddAttackCard(auraCard, 2);
    }
}
