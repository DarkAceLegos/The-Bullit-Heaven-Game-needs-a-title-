using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private PackOfCards product;
    [SerializeField] private int price;
    [SerializeField] private bool singleBuy;
    [SerializeField] private bool sellable;
    [SerializeField] private TextMeshProUGUI typeOfProduct;
    [SerializeField] private string textOfTheTypeOfProduct;

    private void Awake()
    {
        buyButton.onClick.AddListener(() =>
        {
            PlayerMetaProgression.Instance.ChangeCoinAmount(-price);
            gainBuyableItem();
            if (singleBuy) {
                Destroy(this);
            }
        });
    }

    private void Start()
    {
        priceText.text = "Cost: " + price.ToString();
        typeOfProduct.text = textOfTheTypeOfProduct;
    }

    private void gainBuyableItem()
    {
        product.OpenPack();
    }
}
