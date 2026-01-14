using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseMetaAttackAddButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public AttackData attackData;
    [SerializeField] public TextMeshProUGUI attackNameText;
    [SerializeField] public int cost = 10;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] CoinsTextUi coinsText;


    private void Start()
    {
        attackNameText.text = attackData.name;
        costText.text = "Cost: " + cost.ToString();
        updateVisuals();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (PlayerMetaProgression.Instance.coins < cost) { return; }
            //Debug.Log("Changing stat " +  statId + " by the amount " + changeAmount);
            if (!PlayerMetaProgression.Instance.AddAttack(attackData.attackId)) { return; }
            DataPersistenceManager.Instance.SaveGame();
            Debug.Log("Not returned");
            PlayerMetaProgression.Instance.ChangeCoinAmount(-cost);
            updateVisuals();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Changing stat " + statId + " by the amount " + -changeAmount);
            if (!PlayerMetaProgression.Instance.RemoveAttack(attackData.attackId)) { return; }
            DataPersistenceManager.Instance.SaveGame();
            Debug.Log("not returned");
            PlayerMetaProgression.Instance.ChangeCoinAmount(cost);
            updateVisuals();
        }
    }

    private void updateVisuals()
    {
        coinsText.UpdateVisual();

        if (PlayerMetaProgression.Instance.allAttacksPlayerUnlocked.Contains(attackData.attackId))
        {
            GetComponent<Image>().color = Color.green;
        }
        else { GetComponent<Image>().color = Color.white; }
    }
}
