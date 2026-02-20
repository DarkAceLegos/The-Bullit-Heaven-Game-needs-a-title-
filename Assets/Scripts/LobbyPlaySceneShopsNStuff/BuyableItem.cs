using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    [SerializeField] Button buyButton;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] PackOfCards product;
    [SerializeField] int price;
    [SerializeField] bool singleBuy;
    [SerializeField] bool sellable;

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
    }

    private void gainBuyableItem()
    {
        product.OpenPack();
    }
}
