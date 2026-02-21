using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInBinderPrefab : MonoBehaviour
{
    [SerializeField] private Image baseCard;
    [SerializeField] private Image cardSprite;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescrition;
    [SerializeField] private TextMeshProUGUI amountOfTheCard;
    [SerializeField] private string cardId;
}
