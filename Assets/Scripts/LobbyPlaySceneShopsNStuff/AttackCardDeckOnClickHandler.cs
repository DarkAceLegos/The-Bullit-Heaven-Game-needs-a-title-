using UnityEngine;

public class AttackCardDeckOnClickHandler : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        /*AttackCard attackCard = ScriptableObject.CreateInstance<AttackCard>();
        attackCard = (AttackCard)card;

        Debug.Log(attackCard.attackId);

        PlayerMetaProgression.Instance.RemoveAttackCard(attackCard, 1);*/
        

        if (card is AttackCard)
        {
            PlayerMetaProgression.Instance.RemoveAttackCard((AttackCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Attack Card");
        }

        AttackDeckUi.Instance.ShowDeck();
    }
}
