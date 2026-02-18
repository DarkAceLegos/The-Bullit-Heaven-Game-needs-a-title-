using UnityEngine;

public class Cards : ScriptableObject
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public Sprite cardBackground;
    [SerializeField] public Sprite cardForeground;
    [SerializeField] public Sprite foilEffect;
    [SerializeField] public bool isFoil;
}
