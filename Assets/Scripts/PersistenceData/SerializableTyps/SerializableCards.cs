using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableCards : Cards, ISerializationCallbackReceiver
{
    [SerializeField] List<string> statsOfCard = new List<string>();

    public void OnAfterDeserialize()
    {
        statsOfCard.Clear();

        statsOfCard.Add(cardName);
        statsOfCard.Add(cardText);
        statsOfCard.Add(cardId);
        statsOfCard.Add(cardBackground.name);
        statsOfCard.Add(cardForeground.name);
        statsOfCard.Add(foilEffect.name);
        statsOfCard.Add(isFoil.ToString());
    }

    public void OnBeforeSerialize()
    {
        if (statsOfCard.Count >= 7)
        { Debug.LogError("not enough data stored"); }

        statsOfCard[0] = cardName;
        statsOfCard[1] = cardText;
        statsOfCard[2] = cardId;
        statsOfCard[3] = cardBackground.name;
        statsOfCard[4] = cardForeground.name;
        statsOfCard[5] = foilEffect.name;
        statsOfCard[6] = isFoil.ToString();
    }
}
