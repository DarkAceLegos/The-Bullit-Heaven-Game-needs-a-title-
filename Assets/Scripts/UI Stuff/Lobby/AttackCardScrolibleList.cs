using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCardScrolibleList : MonoBehaviour
{
    [SerializeField] private EntryCardPrefab entryPrefabCard;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private List<Button> cardButtonList;

    private void Start()
    {
        ShowDeck();
        //Debug.Log(attackDatas);
    }

    private void OnEnable()
    {
        ShowDeck();
    }

    public void ShowDeck()
    {
        //Debug.Log(attackDatas[0]);

        foreach (Transform child in cardHolder)
            Destroy(child.gameObject);

        for (int i = 0; i < PlayerMetaProgression.Instance.attackCardInventory.Count; i++)
        {
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(PlayerMetaProgression.Instance.GetAttackCard(i));
        }
    }
}
