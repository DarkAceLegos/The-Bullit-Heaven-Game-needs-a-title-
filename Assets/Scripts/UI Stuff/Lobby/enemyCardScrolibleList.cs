using System.Collections.Generic;
using UnityEngine;

public class enemyCardScrolibleList : MonoBehaviour
{
    [SerializeField] private EntryCardPrefab entryPrefabCard;
    [SerializeField] private Transform cardHolder;


    private void Start()
    {
        ShowDeck();
        //Debug.Log(attackDatas);
    }

    public void ShowDeck()
    {
        //Debug.Log(attackDatas[0]);

        foreach (Transform child in cardHolder)
            Destroy(child.gameObject);

        //var randomCards = new List<Cards>();

        foreach (var card in PlayerMetaProgression.Instance.cardsTesting)
        {
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(card);
        }

        /*foreach (var card in randomCards)
        {
            //Debug.Log(card.cardName);
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(card);
        }//*/
    }
}
