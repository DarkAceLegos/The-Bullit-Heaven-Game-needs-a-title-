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

        for (int i = 0; i < PlayerMetaProgression.Instance.enemyCardInventory.Count; i++)
        {
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(PlayerMetaProgression.Instance.GetEnemyCard(i));
        }
    }
}
