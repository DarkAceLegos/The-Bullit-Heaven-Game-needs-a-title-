using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeializableEnemyCard 
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public string cardBackgroundName;
    [SerializeField] public string cardForegroundName;
    [SerializeField] public string foilEffectName;
    [SerializeField] public bool isFoil;
    [SerializeField] public int amountOfPacks; 
    [SerializeField] public int packsSize; 
    [SerializeField] public int typeOfEnemy;
}
