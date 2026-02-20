using System.Collections.Generic;
using UnityEngine;

public class PackOpeningUi : MonoBehaviour
{
    [SerializeField] private Transform upgradeHolder;
    [SerializeField] private EntryCardPrefab entryPrefabCard;

    private List<Cards> set;
    private int cardsInPack;

    private void Start()
    {
        Hide();
    }

    public void Show(List<Cards> setCards, int maxCardsInPack)
    {
        set = setCards;
        Debug.Log(setCards.Count);
        Debug.Log(set.Count);

        cardsInPack = maxCardsInPack;

        ShowCardsInPack();

        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowCardsInPack()
    {
        foreach (Transform child in upgradeHolder)
            Destroy(child.gameObject);

        Debug.Log("Opened Pack is clear");

        List<Cards> availableCards = set;

        if (availableCards == null || availableCards.Count <= 0)
        {
            Debug.Log("no available Cards to obtain");
            //return;            
        }

        var randomCards = new List<Cards>();

        while (randomCards.Count < cardsInPack && availableCards.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
            randomCards.Add(availableCards[randomIndex]);
            availableCards.RemoveAt(randomIndex);
        }

        foreach (var card in randomCards)
        {
            var entry = Instantiate(entryPrefabCard, upgradeHolder);
            entry.Init(card);//*/
        }
    }
}
