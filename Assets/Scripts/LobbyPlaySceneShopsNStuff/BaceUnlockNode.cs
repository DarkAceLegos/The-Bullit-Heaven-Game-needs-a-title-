using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaceUnlockNode : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] private string nameOfUnlock;
    [SerializeField] private string desctiption;
    [SerializeField] public int cost = 10;
    [SerializeField] private int baseCost;
    [SerializeField] private List<Transform> unlockedThings;
    [SerializeField] public int statId;
    [SerializeField] public float changeAmount;
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] public bool unlocked = false;

    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI desctiptionText;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] CoinsTextUi coinsText;

    private void Start()
    {
        baseCost = cost;
        UpdatPrice();
        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (unlocked) { return; }
            if (PlayerMetaProgression.Instance.coins < cost) { return; }
            Unlock();
            PlayerMetaProgression.Instance.ChangeCoinAmount(-cost);
            UpdatPrice();
            UpdateVisual();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            UpdateVisual();
        }
    }
    private void UpdateVisual()
    {
        nameText.text = nameOfUnlock;
        desctiptionText.text = desctiption;
        costText.text = "Cost: " + cost.ToString();
        coinsText.UpdateVisual();
    }

    private void UpdatPrice()
    {
        if (changeAmount != 0)
        {
            cost = (baseCost * (int)PlayerMetaProgression.Instance.GetAmontOfStat(statId)) + baseCost;
        }
    }

    private void Unlock()
    {
        if (unlockedThings.Count > 0)
        {
            foreach (Transform t in unlockedThings)
            {
                t.gameObject.SetActive(true);
                unlocked = true;
            }
            GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("changing Stat");
            PlayerMetaProgression.Instance.ChangeStat(statId, changeAmount);
        }
    }

    public void LoadData(GameData progression)
    {
        progression.unlocks.TryGetValue(id, out unlocked);
        if (unlocked)
        {
            foreach (Transform t in unlockedThings)
            {
                t.gameObject.SetActive(true);
            }
            GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            foreach(Transform t in unlockedThings)
                { t.gameObject.SetActive(false); }
        }
    }

    public void SaveData(ref GameData progression)
    {
        if (progression.unlocks.ContainsKey(id))
        {
            progression.unlocks.Remove(id);
        }
        progression.unlocks.Add(id, unlocked);
    }
}

