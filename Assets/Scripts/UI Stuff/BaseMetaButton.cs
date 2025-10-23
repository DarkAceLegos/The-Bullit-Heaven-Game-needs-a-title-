using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMetaButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public int statId;
    [SerializeField] public float changeAmount;

    [SerializeField] public TextMeshProUGUI statText;

    private void Start()
    {
        statText.text = PlayerMetaProgression.Instance.GetNameOfStat(statId);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Changing stat " +  statId + " by the amount " + changeAmount);
            PlayerMetaProgression.Instance.ChangeStat(statId, changeAmount);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Changing stat " + statId + " by the amount " + -changeAmount);
            PlayerMetaProgression.Instance.ChangeStat(statId, -changeAmount);
        }
    }
}
