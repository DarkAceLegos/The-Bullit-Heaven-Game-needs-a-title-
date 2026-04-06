using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCrafter : MonoBehaviour
{
    public static CardCrafter instance;

    [SerializeField] private List<AttackCard> commonAttackCards;
    [SerializeField] private List<AttackCard> uncommonAttackCards;
    [SerializeField] private List<AttackCard> rareAttackCards;

    [SerializeField] private List<EnemyHealth> typesOfEnemys;
    [SerializeField] private List<Sprite> cardFrames;
    [SerializeField] private List<Sprite> cardFoilEffects;

    private void Awake()
    {
        instance = this;
    }

    public Cards GetARandomCard()
    {
        Cards cards = new Cards();

        int choose = UnityEngine.Random.Range(0, 2);

        if (choose == 0)
        { cards = GetARandomAttackCard(); }
        else if (choose == 1) 
        { cards = GetARandomEnemyCard(); }
        else 
        { cards = GetARandomStatCard(); }

        return cards;
    }

    public AttackCard GetARandomAttackCard()
    {
        //AttackCard card = ScriptableObject.CreateInstance<AttackCard>();
        
        int i = UnityEngine.Random.Range(0, uncommonAttackCards.Count - 1);
        uncommonAttackCards[i].isFoil = false; // AtSome point add a random bool
        return uncommonAttackCards[i];
    }

    public EnemyCard GetARandomEnemyCard()
    {
        EnemyCard card = ScriptableObject.CreateInstance<EnemyCard>();

        int randomEnemyNum = UnityEngine.Random.Range(1, 100);
        
        EnemyHealth theEnemy;

        if (randomEnemyNum == 0)
        {
            theEnemy = typesOfEnemys[typesOfEnemys.Count - 1];
            card.typeOfEnemy = typesOfEnemys.Count - 1;
        }
        else
        {
            int randomTypeOfEnemy = UnityEngine.Random.Range(0, typesOfEnemys.Count - 2);

            theEnemy = typesOfEnemys[randomTypeOfEnemy];

            card.typeOfEnemy = randomTypeOfEnemy;
        }

        card.cardName = theEnemy.name;
        card.cardId = Guid.NewGuid().ToString();
        card.cardBackground = cardFrames[UnityEngine.Random.Range(0, cardFrames.Count - 1)];
        card.cardForeground = theEnemy.GetComponentInChildren<SpriteRenderer>().sprite;
        card.foilEffect = cardFoilEffects[UnityEngine.Random.Range(0, cardFoilEffects.Count - 1)];
        card.isFoil = false; // AtSome point add a random bool
        card.amountOfPacks = UnityEngine.Random.Range(1, 10);
        card.packsSize = UnityEngine.Random.Range(1, 10);
        card.cardText = $"Spawn {card.cardName} with a pack size of {card.packsSize}, {card.amountOfPacks} times.";

        return card;
    }

    public StatCard GetARandomStatCard()
    {
        StatCard card = ScriptableObject.CreateInstance<StatCard>();

        card.cardId = Guid.NewGuid().ToString();
        card.cardBackground = cardFrames[UnityEngine.Random.Range(0, cardFrames.Count - 1)];
        card.foilEffect = cardFoilEffects[UnityEngine.Random.Range(0, cardFoilEffects.Count - 1)];
        card.isFoil = false; // AtSome point add a random bool

        int randomChanceForType = UnityEngine.Random.Range(0, 100);

        if (randomChanceForType >= 25)
        {
            List<int> additiveStat = new List<int> { 0, 2, 5, 6, 10, 14, 16, 18, 20 }; // this is teriable

            card.statId.Add(additiveStat[UnityEngine.Random.Range(0, additiveStat.Count - 1)]);
            if (card.statId[0] == 5)
            {
                card.statChangeAmount.Add(UnityEngine.Random.Range(1, 2));
            }
            else
            {
                card.statChangeAmount.Add(UnityEngine.Random.Range(1, 10));
            }
        }
        else
        {
            List<int> percentiageStat = new List<int> { 1, 3, 4, 7, 8, 9, 11, 12, 13, 15, 17, 19, 21 }; // this is teriable

            card.statId.Add(percentiageStat[UnityEngine.Random.Range(0, percentiageStat.Count - 1)]);

            card.statChangeAmount.Add((UnityEngine.Random.Range(1, 10) 
                + UnityEngine.Random.Range(1, 10) 
                + UnityEngine.Random.Range(1, 10) 
                + UnityEngine.Random.Range(1, 10) 
                + UnityEngine.Random.Range(1, 10)) / 100f);
        }

        card.cardForeground = cardFrames[0];

        card.cardName = PlayerMetaProgression.Instance.GetNameOfStat(card.statId[0]);

        card.cardText = $"Increase {card.cardName} by {card.statChangeAmount[0]}";

        //card.cardForeground = //need to figue out

        return card;
    }
}
