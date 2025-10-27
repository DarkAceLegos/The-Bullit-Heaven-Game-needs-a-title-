using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMetaButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public int statId;
    [SerializeField] public float changeAmount;
    [SerializeField] public int cost = 10;

    [SerializeField] public TextMeshProUGUI statNameText;
    [SerializeField] public TextMeshProUGUI statText;
    [SerializeField] public TextMeshProUGUI costText;

    private void Start()
    {
        statNameText.text = PlayerMetaProgression.Instance.GetNameOfStat(statId);
        statText.text = PlayerMetaProgression.Instance.GetAmontOfStat(statId).ToString();
        costText.text = "Cost: " + cost.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(PlayerMetaProgression.Instance.coins < cost) {return; }
            //Debug.Log("Changing stat " +  statId + " by the amount " + changeAmount);
            PlayerMetaProgression.Instance.ChangeStat(statId, changeAmount);
            DataPersistenceManager.Instance.SaveGame();
            PlayerMetaProgression.Instance.ChangeCoinAmount(-cost);
            cost = cost + cost;
            UpdateVisual();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Changing stat " + statId + " by the amount " + -changeAmount);
            PlayerMetaProgression.Instance.ChangeStat(statId, -changeAmount);
            DataPersistenceManager.Instance.SaveGame();
            PlayerMetaProgression.Instance.ChangeCoinAmount(cost);
            cost = cost - (cost/2);
            if(cost == 0) { cost = 1;}
            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        statText.text = PlayerMetaProgression.Instance.GetAmontOfStat(statId).ToString();
        costText.text = "Cost: " + cost.ToString();
    }
}
