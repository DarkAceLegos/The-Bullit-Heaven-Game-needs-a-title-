using TMPro;
using UnityEngine;

public class StatDeckUi : MonoBehaviour
{
    [SerializeField] private EntryCardPrefab entryPrefabCard;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private TextMeshProUGUI deckAmount;
    
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

        for (int i = 0; i < PlayerMetaProgression.Instance.statCardDeck.Count; i++)
        {
            var entry = Instantiate(entryPrefabCard, cardHolder);
            entry.Init(PlayerMetaProgression.Instance.GetStatCard(i, 1));
        }

        deckAmount.text = (PlayerMetaProgression.Instance.statCardDeck.Count + " / " + PlayerMetaProgression.Instance.maxStatCards);
    }
}
