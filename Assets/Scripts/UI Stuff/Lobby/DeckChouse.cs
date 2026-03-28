using System;
using UnityEngine;

public class DeckChouse : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] public bool unlocked;

    public void LoadData(GameData progression)
    {
        progression.unlocks.TryGetValue(id, out unlocked);
        TryGetComponent<MetaProgressionUi>(out MetaProgressionUi ui);
        Debug.Log(ui);
        if(unlocked)
        {
            ui.Hide();
        }
        else
        {
            ui.Show();
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
