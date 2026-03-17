using System.Collections.Generic;
using UnityEngine;

public class PlayersDeck : MonoBehaviour//, IDataPersistence
{
    [SerializeField] public List<Cards> deckOfCardsAttacks = new();
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
    }

    /*public void LoadData(GameData progression)
    {
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
    }

    public void SaveData(ref GameData progression)
    {
        //throw new System.NotImplementedException();
    }*/
}
