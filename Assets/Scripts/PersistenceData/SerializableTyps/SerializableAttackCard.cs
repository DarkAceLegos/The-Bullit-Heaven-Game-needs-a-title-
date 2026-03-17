using UnityEngine;

[System.Serializable]
public class SerializableAttackCard 
{
    [SerializeField] public string cardName;
    [SerializeField] public string cardText;
    [SerializeField] public string cardId;
    [SerializeField] public string cardBackgroundName;
    [SerializeField] public string cardForegroundName;
    [SerializeField] public string foilEffectName;
    [SerializeField] public bool isFoil;
    [SerializeField] public string attackId;
}
