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

    private bool isFoiled;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GetComponentInParent<CardOnClickHandler>().ActivatCard();
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
    }

    private void Start()
    {
        if (!isFoiled)
        {
            foilGameobject.SetActive(false);
        }
    }
}
