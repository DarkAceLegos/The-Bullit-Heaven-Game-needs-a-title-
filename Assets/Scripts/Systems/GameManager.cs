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

    private float acuualSpawnInterval => spawnInterval / NetworkManager.Singleton.ConnectedClients.Count; //move to game manager
    [SerializeField] private List<EnemyHealth> spawnedEnemies = new(); //move to game manager
    private float lastSpawnTime; //move to game manager

    public event EventHandler<AfterXTimeEventArgs> AfterXTime;
    public class AfterXTimeEventArgs : EventArgs {
        public NetworkObject player;
    }

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

}
