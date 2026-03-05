using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeializableEnemyCard 
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public Sprite cardBackground;
    [SerializeField] public Sprite cardForeground;
    [SerializeField] public Sprite foilEffect;
    [SerializeField] public bool isFoil;
    [SerializeField] public int amountOfPacks; 
    [SerializeField] public int packsSize; 
    [SerializeField] public int typeOfEnemy;
}
