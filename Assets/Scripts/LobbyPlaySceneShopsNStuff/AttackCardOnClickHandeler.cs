using UnityEngine;

public class AttackCardOnClickHandeler : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        /*if (PlayerMetaProgression.Instance.allAttacksPlayerUnlocked.Count >= PlayerMetaProgression.Instance.maxAttackCards) { return; }

        AttackCard attackCard = ScriptableObject.CreateInstance<AttackCard>();
        attackCard = (AttackCard)card;

        //Debug.Log(attackCard.attackId);

        PlayerMetaProgression.Instance.AddAttackCard(attackCard, 1);
        //PlayerMetaProgression.Instance.AddAttack(attackCard.attackId);*/

        if (card is AttackCard)
        {
            PlayerMetaProgression.Instance.AddAttackCard((AttackCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Enemy Card");
        }

        AttackDeckUi.Instance.ShowDeck();
    }
}
