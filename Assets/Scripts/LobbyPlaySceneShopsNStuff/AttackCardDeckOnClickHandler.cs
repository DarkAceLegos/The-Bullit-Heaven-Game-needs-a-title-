using UnityEngine;

public class AttackCardDeckOnClickHandler : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        AttackCard attackCard = ScriptableObject.CreateInstance<AttackCard>();
        attackCard = (AttackCard)card;

        Debug.Log(attackCard.attackId);

        PlayerMetaProgression.Instance.RemoveAttack(attackCard.attackId);
        AttackDeckUi.Instance.ShowDeck();
    }
}
