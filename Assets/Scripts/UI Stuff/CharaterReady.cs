using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharaterReady : NetworkBehaviour
{
    public static CharaterReady instance { get; private set; }

    private Dictionary<ulong, bool> playerReadyDictionary;

    private void Awake()
    {
        instance = this;
        playerReadyDictionary = new Dictionary<ulong, bool>();
    }

    public void SetPlayerReady()
    {
        Debug.Log(NetworkManager.LocalClientId);

        SetPlayerReadyServerRpc(NetworkManager.LocalClientId);
    }

    [Rpc(SendTo.Server)]
    //[ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ulong clientId)
    {
        Debug.Log(clientId);
        Debug.Log(NetworkManager.Singleton.ConnectedClientsIds[0]);

        playerReadyDictionary[clientId] = true;

        bool allClientsReady = true;
        foreach (ulong _clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDictionary.ContainsKey(_clientId) || !playerReadyDictionary[_clientId])
            {
                allClientsReady = false;
                break;
            }
        }

        if (allClientsReady) 
        {
            Loader.LoadNetwork(Loader.Scene.LevelScene);
        }
    }
}
