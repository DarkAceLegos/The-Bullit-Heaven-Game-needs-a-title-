using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [System.Serializable]
    public struct LevelUps
    { public int statId; public int value; }

    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] public bool unlocked;
    [SerializeField] public int cost;
    [SerializeField] private bool startingNode;
    [SerializeField] public bool clickable;
    [SerializeField] private List<LevelUps> levelUps;
        
    [SerializeField] private List<SkillNode> conections;

    public void LoadData(GameData progression)
    {
        progression.skillTree.TryGetValue(id, out unlocked);
        if(startingNode ) { unlocked = true; clickable = true; }
        if (unlocked)
        {
            Show();
            GetComponent<Image>().color = Color.green;

            //ShowConections();
        }
        else { Hide(); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(!clickable) { return; }
            if (PlayerMetaProgression.Instance.coins < cost) { return; }
            if (unlocked) { return; }
            GetComponent<Image>().color = Color.green;
            ShowConections();
            unlocked = true;
            PlayerMetaProgression.Instance.ChangeCoinAmount(-cost);
            foreach (var level in levelUps)
            {
                PlayerMetaProgression.Instance.ChangeStat(level.statId, level.value);
            }
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
}
