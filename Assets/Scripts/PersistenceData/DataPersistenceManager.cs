using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    GameData data;
    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandeler dataHandeler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the sceen.");
        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandeler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = (IEnumerable<IDataPersistence>)FindObjectsByType(typeof(IDataPersistence), FindObjectsSortMode.None);
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        this.data = new GameData();
    }

    public void LoadGame()
    {
        this.data = dataHandeler.Load();

        if (this.data != null)
        {
            Debug.Log("No data was found. Initializing data to default.");
            NewGame();
        }

        foreach(IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.LoadData(data);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.SaveData(ref data);
        }

        dataHandeler.Save(data);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }
}
