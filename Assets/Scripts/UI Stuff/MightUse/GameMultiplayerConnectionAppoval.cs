using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMultiplayerConnectionAppoval : NetworkBehaviour
{
    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if(SceneManager.GetActiveScene().name != Loader.Scene.LobbyPlayScene.ToString())
        {
            //connectionAp
        }
    }*/
}
