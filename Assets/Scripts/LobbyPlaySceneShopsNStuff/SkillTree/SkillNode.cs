using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [System.Serializable]
    public struct LevelUps
    { public stats stats; public float value; }

    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] public bool unlocked;
    [SerializeField] public int cost;
    [SerializeField] private bool startingNode;
    [SerializeField] public bool clickable;
    [SerializeField] public int order = 0;
    [SerializeField] private List<LevelUps> levelUps;

    [SerializeField] private List<SkillNode> conections;

    public enum stats
    {
        additiveMaxHealthModifier = 0,
        percentageMaxHealthModifier = 1,
        additiveDamageModifier = 2,
        percentageDamageModifier = 3,
        percentageCooldownModifier = 4,
        additiveProjectileModifier = 5,
        additiveAreaModifier = 6,
        percentageAreaModifier = 7,
        enemySpawnModifier = 8,
        enemyDamageModifier = 9,
        playerHealthRegen = 10,
        percentagePlayerHealthRegen = 11,
        percentageTreasureFind = 12,
        percentageTreasurGain = 13,
        additivePlayerMoveSpeed = 14,
        percentagePlayerMoveSpeed = 15,
        additiveProjectileSpeed = 16,
        percentageProjectileSpeed = 17,
        additiveDuration = 18,
        percentageDuration = 19,
        additiveExperience = 20,
        percentageExperience = 21,
    }

    public void LoadData(GameData progression)
    {
        progression.skillTree.TryGetValue(id, out unlocked);
        if(startingNode ) { unlocked = true; clickable = true; }
        if (unlocked)
        {
            Show();
            GetComponent<Image>().color = Color.green;
            clickable = true;

            //ShowConections();
        }
        else { Hide(); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(!clickable) { return; }
            if (unlocked) { return; }
            if (PlayerMetaProgression.Instance.coins < SkillCost.Instance.cost) 
            {
                if (PlayerMetaProgression.Instance.spentSkillPoints <= 0)
                { return; }
            }
            GetComponent<Image>().color = Color.green;
            ShowConections();
            unlocked = true;
            if (PlayerMetaProgression.Instance.coins > SkillCost.Instance.cost && PlayerMetaProgression.Instance.spentSkillPoints <= 0)
            { 
                PlayerMetaProgression.Instance.ChangeCoinAmount(-SkillCost.Instance.cost);
                PlayerMetaProgression.Instance.ChangeStat(100, 1);
            }
            PlayerStatScreen.Instance.UpdateViuals();
            PlayerMetaProgression.Instance.spentSkillPoints -= 1;
            SkillCost.Instance.IncreaseCost();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            //if (!unlocked) { return; }
            //GetComponent<Image>().color = Color.white;
        }
    }

    public void SaveData(ref GameData progression)
    {
        if(progression.skillTree.ContainsKey(id))
        {
            progression.skillTree.Remove(id);
        }

        progression.skillTree.Add(id, unlocked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        clickable = false;
    }

    public void ShowConections()
    {
        foreach(var conections in conections)
        {
            conections.Show();
            conections.clickable = true;
        }
    }

    public void StatSkillNodeComp()
    {
        if (levelUps != null)
        {        
            foreach (var level in levelUps)
            {
                PlayerMetaProgression.Instance.ChangeStat((int)level.stats, level.value);
            }
        }

        TryGetComponent<AltSkillNodes>(out var altSkillNodes);
        if (altSkillNodes != null)
            altSkillNodes.SkillUpgrade();
    }

    public string GetDescription()
    {
        string description = "";

        if (levelUps != null)
        {
            foreach (var level in levelUps)
            {
                description += "Add " + level.value + " to " + PlayerMetaProgression.Instance.GetNameOfStat((int)level.stats) + "\n";
            }
        }

        TryGetComponent<AltSkillNodes>(out var altSkillNodes);
        if (altSkillNodes != null)
            description += "and " + altSkillNodes.GetDescription();

        return description;
    }
}
