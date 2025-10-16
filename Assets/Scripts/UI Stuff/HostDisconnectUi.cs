using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectUi : MonoBehaviour
{
    [SerializeField] Button playAgainButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            GameLobby.instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        //Debug.Log(clientId);
        //Debug.Log(NetworkManager.ServerClientId);

        if (!NetworkManager.Singleton.IsServer)
        {
            //Debug.Log("Host Disconnected");
            //Server is shutting down
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}
