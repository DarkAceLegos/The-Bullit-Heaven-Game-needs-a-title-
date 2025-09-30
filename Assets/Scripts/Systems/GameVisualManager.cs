using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class GameVisualManager : NetworkBehaviour
{

    [SerializeField] private List<Transform> visualObjectsPrefabs = new();

    [SerializeField] private List<EnemyHealth> enemies = new();
    [SerializeField] private List<Transform> spawnPoints = new();

    private void Start()
    {
        GameManager.Instance.AfterXTime += Instance_AfterXTime;
    }

    private void Instance_AfterXTime(object sender, GameManager.AfterXTimeEventArgs e)
    {
        SpawnEnemyRpc(e.player);

    }

    [Rpc(SendTo.Server)]
    private void SpawnEnemyRpc(NetworkObjectReference playerReference)
    {
        playerReference.TryGet(out NetworkObject player);

        if(enemies.Count <= 0 || spawnPoints.Count <= 0)
        {
            Debug.Log("No enemies or spawn points");
            return;
        }

        var spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        var enemy = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Count)], spawnPosition + player.transform.position, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn(true);
        NetworkObject enemyNetworkObject =  enemy.GetComponent<NetworkObject>();
        AddEnemyToListRpc(enemyNetworkObject);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void AddEnemyToListRpc(NetworkObjectReference enemy)
    {
        enemy.TryGet(out NetworkObject enemyObject);

        EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();

        GameManager.Instance.AddEnemyToList(enemyHealth);
    }

    public override void OnDestroy()
    {
        GameManager.Instance.AfterXTime -= Instance_AfterXTime;
    }
}
