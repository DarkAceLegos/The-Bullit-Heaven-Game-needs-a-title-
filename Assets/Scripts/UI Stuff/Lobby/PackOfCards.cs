using System.Collections.Generic;
using UnityEngine;

public class PackOfCards : MonoBehaviour
{
    [SerializeField] private List<Cards> set;
    [SerializeField] private PackOpeningUi packOpeningUi;
    [SerializeField] private int maxCardsInPack;

    public List<Cards> GetSet() { 
        List<Cards> cards = new List<Cards>(set);
        return cards; 
    }

    public void OpenPack()
    {
        Debug.Log(set.Count);
        Debug.Log("trying to open a pack");
        Debug.Log(set);
        Debug.Log(maxCardsInPack);
        packOpeningUi.Show(GetSet(), maxCardsInPack);
        Debug.Log(set.Count);
    }
}
