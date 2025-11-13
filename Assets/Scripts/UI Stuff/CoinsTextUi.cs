using TMPro;
using UnityEngine;

public class CoinsTextUi : MonoBehaviour
{
    public static CoinsTextUi Instance;

    [SerializeField] TextMeshProUGUI amountText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        amountText.text = "Coins " + PlayerMetaProgression.Instance.coins.ToString();
    }

    public void UpdateVisual()
    {
        amountText.text = "Coins " + PlayerMetaProgression.Instance.coins.ToString();
    }
}
