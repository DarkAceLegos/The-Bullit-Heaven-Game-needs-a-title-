using TMPro;
using UnityEngine;

public class SkillCost : MonoBehaviour
{
    public static SkillCost Instance;

    [SerializeField] private int startingCost;
    [SerializeField] public int cost;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI skillPoints;
    [SerializeField] private CoinsTextUi CoinsTextUi;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        cost = (startingCost * PlayerMetaProgression.Instance.maxSkillPoints) + startingCost;
        UpdateVisuals();
    }

    public void IncreaseCost()
    {
        cost = (startingCost * PlayerMetaProgression.Instance.maxSkillPoints) + startingCost;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        costText.text = cost.ToString();
        skillPoints.text = (PlayerMetaProgression.Instance.spentSkillPoints + " / " + PlayerMetaProgression.Instance.maxSkillPoints);
        CoinsTextUi.UpdateVisual();
    }
}
