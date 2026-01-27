using UnityEngine;

public class Cards : ScriptableObject
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private Sprite cardForeground;
}
