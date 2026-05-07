using System.Collections.Generic;
using UnityEngine;

public class PlayersDeck : MonoBehaviour//, IDataPersistence
{
    [SerializeField] public List<AttackCard> deckOfCardsAttacks = new();
    [SerializeField] public List<EnemyCard> deckOfEnemyCards = new();
    private List<SeializableEnemyCard> enemyCards;

    [SerializeField] public PlayerMetaProgression playerMetas;

    private void Start()
    {
        /*for (int i = 0; i > playerMetas.enemyCardDeck.Count; i++) 
        {
            deckOfEnemyCards.Add(playerMetas.GetEnemyCard(i, 1));
        }
        Debug.Log(deckOfEnemyCards.Count);*/

        enemyCards = playerMetas.enemyCardDeck;

        Debug.Log(enemyCards.Count);

        for(int i = 0; i < enemyCards.Count; i++)
        {
            EnemyCard cards = ScriptableObject.CreateInstance<EnemyCard>();

            cards.cardName = enemyCards[i].cardName;
            cards.cardText = enemyCards[i].cardText;
            cards.cardId = enemyCards[i].cardId;
            cards.cardBackground = Resources.Load<Sprite>("Sprites/" + enemyCards[i].cardBackgroundName);
            cards.cardForeground = Resources.Load<Sprite>("Sprites/" + enemyCards[i].cardForegroundName);
            cards.foilEffect = Resources.Load<Sprite>("Sprites/" + enemyCards[i].foilEffectName);
            cards.isFoil = enemyCards[i].isFoil;
            cards.amountOfPacks = enemyCards[i].amountOfPacks;
            cards.packsSize = enemyCards[i].packsSize;
            cards.typeOfEnemy = enemyCards[i].typeOfEnemy;

            deckOfEnemyCards.Add(cards);
        }

        List<SerializableAttackCard> attackCards;
        attackCards = playerMetas.attackCardDeck;

        for (int i = 0; i < attackCards.Count; i++) 
        { 
            AttackCard card = ScriptableObject.CreateInstance<AttackCard>();

            card.cardName = attackCards[i].cardName;
            card.cardText = attackCards[i].cardText;
            card.cardId = attackCards[i].cardId;
            card.cardBackground = Resources.Load<Sprite>("Sprites/" + attackCards[i].cardBackgroundName);
            card.cardForeground = Resources.Load<Sprite>("Sprites/" + attackCards[i].cardForegroundName);
            card.foilEffect = Resources.Load<Sprite>("Sprites/" + attackCards[i].foilEffectName);
            card.isFoil = attackCards[i].isFoil;
            card.attackId = attackCards[i].attackId;

            deckOfCardsAttacks.Add(card);
        }//*/
    }

    /*public void LoadData(GameData progression)
    {
        Debug.Log("playerDeck Load");

        this.deckOfCardsAttacks.Clear();
        this.deckOfEnemyCards.Clear();
        foreach (var card in progression.enemyCardDeck)
        {
            EnemyCard cards = ScriptableObject.CreateInstance<EnemyCard>();

            cards.cardName = card.cardName;
            cards.cardText = card.cardText;
            cards.cardId = card.cardId;
            cards.cardBackground = Resources.Load<Sprite>("Sprites/" + card.cardBackgroundName);
            cards.cardForeground = Resources.Load<Sprite>("Sprites/" + card.cardForegroundName); ;
            cards.foilEffect = Resources.Load<Sprite>("Sprites/" + card.foilEffectName);
            cards.isFoil = card.isFoil;
            cards.amountOfPacks = card.amountOfPacks;
            cards.packsSize = card.packsSize;
            cards.typeOfEnemy = card.typeOfEnemy;

            deckOfEnemyCards.Add(cards);
        }

        for (int i = 0; i < progression.attackCardDeck.Count; i++)
        {
            AttackCard card = ScriptableObject.CreateInstance<AttackCard>();

            card.cardName = progression.attackCardDeck[i].cardName;
            card.cardText = progression.attackCardDeck[i].cardText;
            card.cardId = progression.attackCardDeck[i].cardId;
            card.cardBackground = Resources.Load<Sprite>("Sprites/" + progression.attackCardDeck[i].cardBackgroundName);
            card.cardForeground = Resources.Load<Sprite>("Sprites/" + progression.attackCardDeck[i].cardForegroundName);
            card.foilEffect = Resources.Load<Sprite>("Sprites/" + progression.attackCardDeck[i].foilEffectName);
            card.isFoil = progression.attackCardDeck[i].isFoil;
            card.attackId = progression.attackCardDeck[i].attackId;

            deckOfCardsAttacks.Add(card);
        }
    }

    public void SaveData(ref GameData progression)
    {
        //throw new System.NotImplementedException();
    }*/
}
