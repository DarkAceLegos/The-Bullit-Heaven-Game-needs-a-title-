using System.Collections.Generic;
using UnityEngine;

public class PackOfCards : MonoBehaviour
{
    [SerializeField] private List<Cards> set;
    [SerializeField] private PackOpeningUi packOpeningUi;
    [SerializeField] private int maxCardsInPack;
    [SerializeField] private bool containsAttackCards;
    [SerializeField] private bool containsEnemyCards;
    [SerializeField] private bool containsStatCards;


    public List<Cards> GetSet()
    {
        
        List<Cards> cards = new List<Cards>();
        
        if (set != null )
        {
            cards = new List<Cards>(set);
        }

        //Debug.Log(maxCardsInPack > cards.Count);



        do
        {
            Debug.Log("Getting more cards");
            if (containsAttackCards & containsEnemyCards & containsStatCards)
            { cards.Add(CardCrafter.instance.GetARandomCard()); }

            else
            {
                if (containsAttackCards)
                { cards.Add(CardCrafter.instance.GetARandomAttackCard()); }

                if (containsEnemyCards)
                { cards.Add(CardCrafter.instance.GetARandomEnemyCard()); }

                if (containsStatCards)
                { cards.Add(CardCrafter.instance.GetARandomStatCard()); }
            }
        } while (cards.Count < maxCardsInPack);

        //Debug.Log(cards.Count);

        return cards; 
    }

    public void OpenPack()
    {
        /*Debug.Log(set.Count);
        Debug.Log("trying to open a pack");
        Debug.Log(set);
        Debug.Log(maxCardsInPack);*/
        packOpeningUi.Show(GetSet(), maxCardsInPack);
        //Debug.Log(set.Count);
    }
}
