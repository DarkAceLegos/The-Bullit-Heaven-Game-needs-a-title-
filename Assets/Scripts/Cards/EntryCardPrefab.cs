using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryCardPrefab : MonoBehaviour
{
    [SerializeField] private Image backgroundArt;
    [SerializeField] private Image art;
    [SerializeField] private Image foilEffect;
    [SerializeField] private GameObject foilGameobject;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private bool isFoiled;

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
