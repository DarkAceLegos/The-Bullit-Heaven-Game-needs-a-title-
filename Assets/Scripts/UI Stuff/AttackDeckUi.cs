using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackDeckUi : MonoBehaviour
{
    public static AttackDeckUi Instance;

    [SerializeField] private EntryCardPrefab entryPrefabCard;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private List<AttackData> attackDatas = new List<AttackData>();
    private AttackCard attackCard;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private TextMeshProUGUI deckAmount;

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
        //Debug.Log(attackDatas[0]);

        foreach (Transform child in cardHolder)
            Destroy(child.gameObject);

        var randomCards = new List<AttackCard>();

        foreach (var id in PlayerMetaProgression.Instance.allAttacksPlayerUnlocked)
        {
            foreach (var attackData in attackDatas)
            {
                if (id == attackData.attackId)
                {
                    attackCard = ScriptableObject.CreateInstance<AttackCard>();

                    attackCard.cardText = attackData.GetLevelDescription(0);
                    //Debug.Log(attackCard.cardText);
                    attackCard.cardName = attackData.name;
                    //Debug.Log(attackCard.cardName);
                    attackCard.cardBackground = cardBackground;
                    attackCard.attackId = id;
                    attackCard.cardForeground = attackData.icon;

                    randomCards.Add(attackCard);
                }
            }
        }

        foreach (var card in randomCards)
        {
            //Debug.Log(card.cardName);
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(card);
        }//*/

        deckAmount.text = (PlayerMetaProgression.Instance.allAttacksPlayerUnlocked.Count + " / " + PlayerMetaProgression.Instance.maxAttackCards);
    }
}
