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
    [SerializeField] private List<Collider2D> spawnPoints = new();

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

        //int timeelemet = ((int)Time.timeSinceLevelLoad);
        //Debug.Log(timeelemet);

        if(enemies.Count <= 0 || spawnPoints.Count <= 0)
        {
            Debug.Log("No enemies or spawn points");
            return;
        }

        var playersDeck = Player.LoaclInstance.GetComponent<PlayersDeck>();

        if (playersDeck.deckOfEnemyCards.Count != 0)
        {
            int i = UnityEngine.Random.Range(0, playersDeck.deckOfEnemyCards.Count);

            //Debug.Log(playersDeck.deckOfEnemyCards[i].amountOfPacks);

            SpawnEnemy(player, playersDeck.deckOfEnemyCards[i].amountOfPacks, playersDeck.deckOfEnemyCards[i].packsSize, playersDeck.deckOfEnemyCards[i].typeOfEnemy + 1);
        }
        else 
        {
            SpawnEnemy(player, UnityEngine.Random.Range(1, 4), UnityEngine.Random.Range(5, 10), UnityEngine.Random.Range(0, 3));
        }

        

        /*var spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        var enemy = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Count)], spawnPosition + player.transform.position, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn(true);
        NetworkObject enemyNetworkObject =  enemy.GetComponent<NetworkObject>();
        AddEnemyToListRpc(enemyNetworkObject);*/
    }

    [Rpc(SendTo.Everyone)]
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

    private void SpawnEnemy(NetworkObject player, int numPlaces, int numAmount, int enemyType)
    {
        Collider2D spawnPosition;
        EnemyHealth enemy;

        for (int i = 0; i < numPlaces; i++)
        {
            spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];

            for (int j = 0; j < numAmount; j++)
            {
                enemy = Instantiate(enemies[enemyType], (Vector3)RandomPointInSpawnArea(spawnPosition) + player.transform.position, Quaternion.identity);
                enemy.GetComponent<NetworkObject>().Spawn(true);
                NetworkObject enemyNetworkObject = enemy.GetComponent<NetworkObject>();
                AddEnemyToListRpc(enemyNetworkObject);
            }//*/
        }
        //var spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        //var enemy = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Count)], spawnPosition + player.transform.position, Quaternion.identity);
        //enemy.GetComponent<NetworkObject>().Spawn(true);
        //NetworkObject enemyNetworkObject = enemy.GetComponent<NetworkObject>();
        //AddEnemyToListRpc(enemyNetworkObject);
    }

    private Vector2 RandomPointInSpawnArea(Collider2D collider)
    {
        Bounds bounds = collider.bounds;

        Vector2 minBounds = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 maxBounds = new Vector2(bounds.max.x, bounds.max.y);

        float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
        float randomY = UnityEngine.Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}
