using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMetaAttackAddButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public AttackData attackData;
    [SerializeField] public TextMeshProUGUI attackText;

    private void Start()
    {
        attackText.text = attackData.name; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Changing stat " +  statId + " by the amount " + changeAmount);
            PlayerMetaProgression.Instance.AddAttack(attackData);
            DataPersistenceManager.Instance.SaveGame();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Changing stat " + statId + " by the amount " + -changeAmount);
            PlayerMetaProgression.Instance.RemoveAttack(attackData);
            DataPersistenceManager.Instance.SaveGame();
        }
    }
}
