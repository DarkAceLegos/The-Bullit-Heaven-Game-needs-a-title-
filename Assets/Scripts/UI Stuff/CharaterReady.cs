using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharaterReady : NetworkBehaviour
{
    public static CharaterReady instance { get; private set; }

    public event EventHandler OnReadyChanged;

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
    private void SetPlayerReadyServerRpc(ulong clientId)
    {
        //Debug.Log(clientId);
        //Debug.Log(NetworkManager.Singleton.ConnectedClientsIds[0]);

        SetPlayerReadyRpc(clientId);

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
            GameLobby.instance.DeleteLobby();
            Loader.LoadNetwork(Loader.Scene.LevelScene);
        }
    }

    [Rpc(SendTo.Everyone)]
    private void SetPlayerReadyRpc(ulong clientId)
    {
        playerReadyDictionary[clientId] = true;

        OnReadyChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsPlayerReady(ulong clientId)
    { 
        return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
    }
}
