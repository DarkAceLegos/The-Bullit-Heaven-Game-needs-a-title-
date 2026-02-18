using System.Collections.Generic;
using UnityEngine;

public class PackOfCards : MonoBehaviour
{
    [SerializeField] private List<Cards> set;
    [SerializeField] private PackOpeningUi packOpeningUi;
    [SerializeField] private int maxCardsInPack;

    public void OpenPack()
    {
        packOpeningUi.Show(set, maxCardsInPack);
    }
}
