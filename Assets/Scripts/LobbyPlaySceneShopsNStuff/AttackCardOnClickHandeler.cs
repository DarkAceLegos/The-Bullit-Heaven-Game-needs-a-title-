using UnityEngine;

public class AttackCardOnClickHandeler : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        if (PlayerMetaProgression.Instance.allAttacksPlayerUnlocked.Count >= PlayerMetaProgression.Instance.maxAttackCards) { return; }

        AttackCard attackCard = ScriptableObject.CreateInstance<AttackCard>();
        attackCard = (AttackCard)card;

        Debug.Log(attackCard.attackId);

        PlayerMetaProgression.Instance.AddAttack(attackCard.attackId);
        AttackDeckUi.Instance.ShowDeck();
    }
}
