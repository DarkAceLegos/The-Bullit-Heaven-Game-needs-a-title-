using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class EntryCardPrefab : MonoBehaviour
{
    [SerializeField] private Image backgroundArt;
    [SerializeField] private Image art;
    [SerializeField] private Image foilEffect;
    [SerializeField] private GameObject foilGameobject;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Cards card;
    private bool isFoiled;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GetComponentInParent<CardOnClickHandler>().ActivatCard(card);
            //Debug.Log(transform.parent.gameObject.transform.parent.gameObject.TryGetComponent<PackOpeningUi>());
            if (transform.parent.gameObject.transform.parent.gameObject.TryGetComponent<PackOpeningUi>(out PackOpeningUi packOpeningUi)) 
            { 
                //Debug.Log(this);
                Destroy(this.gameObject);
                //Debug.Log(packOpeningUi.GetComponentInChildren<PackOnClick>().transform.childCount);
                if(packOpeningUi.GetComponentInChildren<PackOnClick>().transform.childCount == 1)
                { packOpeningUi.Hide(); }
            } 
        });
    }

    public void Init(Cards card)
    {
        backgroundArt.sprite = card.cardBackground;
        art.sprite = card.cardForeground;
        foilEffect.sprite = card.foilEffect;
        isFoiled = card.isFoil;
        nameText.text = card.cardName;
        descriptionText.text = card.cardText;

        this.card = card;
    }

    private void Start()
    {
        if (!isFoiled)
        {
            foilGameobject.SetActive(false);
        }
    }
}
