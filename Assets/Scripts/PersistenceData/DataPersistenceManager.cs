using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandeler dataHandeler;
    private string selectedProfileId = "";

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the sceen.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        this.dataHandeler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);

        this.selectedProfileId = dataHandeler.GetMostRecentlyUpdatedProfileId();
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
        this.gameData = dataHandeler.Load(selectedProfileId);

        if (this.gameData != null && initializeDataIfNull) 
        {
            NewGame();
        }

        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded."); //make an event
            //NewGame();
            return;
        }

        foreach(IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game Needs to be started before data can be saved.");
            return;
        }

        foreach (IDataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        dataHandeler.Save(gameData, selectedProfileId);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
    }

    private void Player_OnAnyPlayerSpawned(object sender, Player.OnAnyPlayerSpawnedEventArgs e)
    {
        Debug.Log("Loading Data for client" + e.clientId);

        bool hasRun = false;

        if (!hasRun)
        {
            this.dataPersistencesObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        hasRun = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Player.OnAnyPlayerSpawned -= Player_OnAnyPlayerSpawned;
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("On Load Scene");
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfileGaemData()
    {
        return dataHandeler.LoadAllProfiles();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }
}
