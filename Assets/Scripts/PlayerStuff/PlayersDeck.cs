using System.Collections.Generic;
using UnityEngine;

public class PlayersDeck : MonoBehaviour
{
    [SerializeField] public List<Cards> deckOfCardsAttacks = new();
    [SerializeField] public List<EnemyCard> deckOfEnemyCards = new();
}
