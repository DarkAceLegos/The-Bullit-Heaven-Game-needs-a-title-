using UnityEngine;

public class StatCardDeckOnClickHandler : CardOnClickHandler
{
    [SerializeField] private StatDeckUi ui;

    public override void ActivatCard(Cards card)
    {
        if (card is StatCard)
        {
            //Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.RemoveStatCard((StatCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Stat Card");
        }

        ui.ShowDeck();
    }
}
