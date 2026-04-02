using UnityEngine;

public class StatCardOnClickHandler : CardOnClickHandler
{
    [SerializeField] private StatDeckUi ui;

    public override void ActivatCard(Cards card)
    {
        if(PlayerMetaProgression.Instance.statCardDeck.Count >= PlayerMetaProgression.Instance.maxStatCards) {return; }

        if (card is StatCard)
        {
            //Debug.Log("Enemy Card");
            PlayerMetaProgression.Instance.AddStatCard((StatCard)card, 1);
        }
        else
        {
            Debug.Log("Not an Stat Card");
        }

        ui.ShowDeck();
    }
}
