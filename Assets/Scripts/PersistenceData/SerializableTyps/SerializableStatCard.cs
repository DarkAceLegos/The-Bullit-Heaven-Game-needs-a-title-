using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableStatCard
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public Sprite cardBackground;
    [SerializeField] public Sprite cardForeground;
    [SerializeField] public Sprite foilEffect;
    [SerializeField] public bool isFoil;
    [SerializeField] public List<int> statId = new List<int>();
    [SerializeField] public List<float> statChangeAmount = new List<float>();

}
