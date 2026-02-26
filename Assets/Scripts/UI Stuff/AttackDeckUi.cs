using System.Collections.Generic;
using UnityEngine;

public class AttackDeckUi : MonoBehaviour
{
    public static AttackDeckUi Instance;

    [SerializeField] private EntryCardPrefab entryPrefabCard;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private List<AttackData> attackDatas = new List<AttackData>();
    [SerializeField] private Cards attackCard;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowDeck();
        //Debug.Log(attackDatas);
    }

    public void ShowDeck()
    {
        Debug.Log(attackDatas[0]);

        foreach (Transform child in cardHolder)
            Destroy(child.gameObject);

        var randomCards = new List<Cards>();

        foreach (var card in attackDatas)
        {
            if (PlayerMetaProgression.Instance.allAttacksPlayerUnlocked.Contains(card.attackId))
            {
                attackCard.cardText = card.GetLevelDescription(0);
                attackCard.cardName = card.name;

                randomCards.Add(attackCard);
            }
        }

        foreach (var card in randomCards)
        {
                var entry = Instantiate(entryPrefabCard, cardHolder);
                entry.Init(card);
        }//*/
    }
}
