using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandeler dataHandeler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the sceen.");
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.dataHandeler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>(); //FindObjectsByType<IDataPersistence>(FindObjectsSortMode.None);
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandeler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to default.");
            NewGame();
        }

        foreach(IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }

        dataHandeler.Save(gameData);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }
}
