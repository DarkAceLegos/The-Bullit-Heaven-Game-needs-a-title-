using UnityEngine;

public class EnemyDeckCardOnClick : CardOnClickHandler
{
    [SerializeField] private EnemyDeckUi ui;

    public override void ActivatCard(Cards card)
    {
        if (card is EnemyCard)
        {
            Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.RemoveEnemyCard((EnemyCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Enemy Card");
        }

        ui.ShowDeck();
    }
}
