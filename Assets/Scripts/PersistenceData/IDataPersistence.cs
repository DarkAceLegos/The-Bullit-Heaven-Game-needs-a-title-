using UnityEngine;

public interface IDataPersistence
{
    void LoadData(PlayerMetaProgression progression);
    void SaveData(ref PlayerMetaProgression progression );
}
