using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Matchmaker.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public List<NetworkObject> playerList = new();
    [SerializeField] private List<EnemyHealth> enemies = new();
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private float spawnInterval = 2f; //move to game manager
    [SerializeField] private int maxEnemies = 20; //move to game manager

    [SerializeField] private Transform playerPrefab;

    private float acuualSpawnInterval => spawnInterval / NetworkManager.Singleton.ConnectedClients.Count; //move to game manager
    [SerializeField] private List<EnemyHealth> spawnedEnemies = new(); //move to game manager
    private float lastSpawnTime; //move to game manager
    private bool isGamePaused = false;

    public event EventHandler<AfterXTimeEventArgs> AfterXTime;
    public class AfterXTimeEventArgs : EventArgs {
        public NetworkObject player;
    }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("More than one GameManager instance");
        }

        Instance = this; 
    }

    private void Start()
    {
        EnemyHealth.onEnemyKilled += OnEnemyKilled; //move to game manager
        Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        GameInputs.Instance.OnPauseAction += GameInputs_OnPauseAction;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
    }
private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab);
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }

    private void GameInputs_OnPauseAction(object sender, EventArgs e)
    {
        TogglePause();
    }

    private void FixedUpdate()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {

        if (spawnedEnemies.Count >= maxEnemies) //move to game manager
            return;

        //Debug.Log("Past first check");

        if (lastSpawnTime + acuualSpawnInterval > Time.time) //move to game manager
            return;

        //Debug.Log("past time needed");

        lastSpawnTime = Time.time; //move to game manager

        //Debug.Log("try to go in the for loop");

        AfterXTime?.Invoke(this, new AfterXTimeEventArgs
        {
            player = playerList[0]
        });
    }

    private void OnEnemyKilled(EnemyHealth enemy)
    {
        //spawnedEnemies.Remove(enemy); //move to game manager

        NetworkObject enemyNetworkObject = enemy.GetComponent<NetworkObject>();

        RemoveEnemyToListRpc(enemyNetworkObject);
    }//*/

    public void AddEnemyToList(EnemyHealth enemy)
    {
        spawnedEnemies.Add(enemy);
    }

    private void Player_OnAnyPlayerSpawned(object sender, Player.OnAnyPlayerSpawnedEventArgs e)
    {
        playerList.Add(e.player);
        Debug.Log("Added " + sender + " to Player list with an id of " + e.clientId);
    }

    [Rpc(SendTo.Everyone)]
    private void RemoveEnemyToListRpc(NetworkObjectReference enemy)
    {
        enemy.TryGet(out NetworkObject enemyObject);

        EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();

        spawnedEnemies.Remove(enemyHealth);
    }
    public override void OnDestroy()
    {
        EnemyHealth.onEnemyKilled -= OnEnemyKilled; //move to game manager
        Player.OnAnyPlayerSpawned -= Player_OnAnyPlayerSpawned;
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            if (NetworkManager.ConnectedClients.Count == 1)
            {
                Time.timeScale = 0f;
            }
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }else
        {
            if (NetworkManager.ConnectedClients.Count == 1)
                Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }

        
    }

}
