using System;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class LobbyPlaySceneManager : NetworkBehaviour
{
    [SerializeField] Transform PlayerPrefabForLobbyPlayScene;
    //[SerializeField] int playerIndex = 0;

    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnPlayerDataNetworkChanged += GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged;
    }

    private void GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged(object sender, EventArgs e)
    {
        Debug.Log("trying to spawn player");
        Transform playerTransform = Instantiate(PlayerPrefabForLobbyPlayScene);
        //PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
        playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId, true);
    }
}
