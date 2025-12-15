using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyPlaySceneManager : NetworkBehaviour
{
    [SerializeField] Transform PlayerPrefabForLobbyPlayScene;

    public override void OnNetworkSpawn()
    {
        Debug.Log("Spawned");
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        NetworkManager.OnConnectionEvent += NetworkManager_OnConnectionEvent;
    }

    private void NetworkManager_OnConnectionEvent(NetworkManager arg1, ConnectionEventData arg2)
    {
        if (!IsServer)
        {
            Debug.Log("trying to spawn player");
            SpawnPlayerInLobbyRPC(NetworkManager.Singleton.LocalClientId);
        }
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("trying to spawn player");
        SpawnPlayerInLobbyRPC(NetworkManager.Singleton.LocalClientId);
    }

    [Rpc(SendTo.Server)]
    private void SpawnPlayerInLobbyRPC(ulong playerId)
    {
        Transform playerTransform = Instantiate(PlayerPrefabForLobbyPlayScene);
        playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerId, true);
    }
}
