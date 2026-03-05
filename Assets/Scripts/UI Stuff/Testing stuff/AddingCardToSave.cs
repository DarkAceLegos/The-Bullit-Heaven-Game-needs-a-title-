using System;
using UnityEngine;
using UnityEngine.UI;

public class AddingCardToSave : MonoBehaviour
{
    [SerializeField] Button addingButtonTest;
    //[SerializeField] EnemyCard enemyCard;

    private void Awake()
    {
        addingButtonTest.onClick.AddListener(() =>
        {
            //PlayerMetaProgression.Instance.testingCard = SpawnEnemyCard();

            PlayerMetaProgression.Instance.AddEnemyCard(SpawnEnemyCard());
        });
    }

    private EnemyCard SpawnEnemyCard()
    {
        EnemyCard card = ScriptableObject.CreateInstance<EnemyCard>();

        card.cardName = "Created Card";
        card.amountOfPacks = UnityEngine.Random.Range(1,10);
        card.packsSize = UnityEngine.Random.Range(1,10);
        card.typeOfEnemy = UnityEngine.Random.Range(1,10);
        card.cardText = "This is a created card Text. With stats of " + card.amountOfPacks + " " + card.packsSize + " " + card.typeOfEnemy;
        card.cardId = Guid.NewGuid().ToString();

        return card;
    }
}
