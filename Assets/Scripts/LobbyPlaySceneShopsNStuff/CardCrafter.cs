using System.Collections.Generic;
using UnityEngine;

public class CardCrafter : MonoBehaviour
{
    public static CardCrafter instance;

    [SerializeField] private List<AttackCard> attackCards;

    private void Awake()
    {
        instance = this;
    }

    public Cards GetARandomCard()
    {
        Cards cards = new Cards();

        int choose = Random.Range(0, 2);

        if (choose == 0)
        { cards = GetARandomAttackCard(); }
        else if (choose == 1) 
        { cards = GetARandomEnemyCard(); }
        else 
        { cards = GetARandomStatCard(); }

        return GetARandomAttackCard();
    }

    public AttackCard GetARandomAttackCard()
    {
        //AttackCard card = ScriptableObject.CreateInstance<AttackCard>();

        int i = Random.Range(0, attackCards.Count - 1);

        return attackCards[i];
    }

    public EnemyCard GetARandomEnemyCard()
    {
        EnemyCard card = ScriptableObject.CreateInstance<EnemyCard>();

        

        return card;
    }

    public StatCard GetARandomStatCard()
    {
        StatCard card = ScriptableObject.CreateInstance<StatCard>();



        return card;
    }
}
