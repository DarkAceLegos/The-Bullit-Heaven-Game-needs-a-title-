using UnityEngine;

public class DeckOnClick : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        Debug.Log("Card Activated");

        if (card is AttackCard)
        {
            Debug.Log("Attack Card");
            PlayerMetaProgression.Instance.AddAttackCard((AttackCard)card);
            PlayerMetaProgression.Instance.AddAttack(card.cardId);
        }
        else if (card is EnemyCard)
        {
            Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.AddEnemyCard((EnemyCard)card);
            PlayerMetaProgression.Instance.AddEnemyCard((EnemyCard)card, 1);
        }
        else if (card is StatCard)
        {
            Debug.Log("Stat Card");
            PlayerMetaProgression.Instance.AddStatCard((StatCard)card);
            PlayerMetaProgression.Instance.AddStatCard((StatCard)card,1);
        }
    }
}
