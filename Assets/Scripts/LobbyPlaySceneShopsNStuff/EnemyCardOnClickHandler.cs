using UnityEngine;

public class EnemyCardOnClickHandler : CardOnClickHandler
{
    [SerializeField] private EnemyDeckUi ui;

    public override void ActivatCard(Cards card)
    {
        if (card is EnemyCard)
        {
            //Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.AddEnemyCard((EnemyCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Enemy Card");
        }

        ui.ShowDeck();
    }
}
