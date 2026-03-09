using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillNode : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private bool unlocked;

    public void LoadData(GameData progression)
    {
        progression.skillTree.TryGetValue(id, out unlocked);
        if (unlocked)
        {
            //Do Something
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            
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
}
