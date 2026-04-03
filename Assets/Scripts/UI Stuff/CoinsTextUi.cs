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
        PlayerMetaProgression.Instance.OnCoinChange += PlayerMetaProgression_OnCoinChange;
    }

    private void PlayerMetaProgression_OnCoinChange(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        amountText.text = "Coins " + PlayerMetaProgression.Instance.coins.ToString();
    }
}
