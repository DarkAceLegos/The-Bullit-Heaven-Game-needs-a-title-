using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableStatCard
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public string cardBackgroundName;
    [SerializeField] public string cardForegroundName;
    [SerializeField] public string foilEffectName;
    [SerializeField] public bool isFoil;
    [SerializeField] public List<int> statId = new List<int>();
    [SerializeField] public List<float> statChangeAmount = new List<float>();

}
