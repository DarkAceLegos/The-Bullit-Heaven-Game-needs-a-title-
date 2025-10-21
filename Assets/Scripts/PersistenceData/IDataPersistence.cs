using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData progression);
    void SaveData(ref GameData progression );
}
