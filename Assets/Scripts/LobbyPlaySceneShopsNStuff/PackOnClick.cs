using UnityEngine;

public class PackOnClick : CardOnClickHandler
{
    public override void ActivatCard(Cards card)
    {
        Debug.Log("Card Activated");

        if (card is AttackCard)
        { 
            Debug.Log("Attack Card");
            PlayerMetaProgression.Instance.AddAttackCard((AttackCard)card);
        }
        else if (card is EnemyCard)
        { 
            Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.AddEnemyCard((EnemyCard)card);
        }
        else if (card is StatCard)
        { 
            Debug.Log("Stat Card");
            PlayerMetaProgression.Instance.AddStatCard((StatCard)card);
        }
    }
}
