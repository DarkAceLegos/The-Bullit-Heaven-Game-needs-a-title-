using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingLobbyUi : MonoBehaviour
{
    [SerializeField] private Button hostGameButton;
    [SerializeField] private Button joinGameButton;

    private void Awake()
    {
        hostGameButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Loader.LoadNetwork(Loader.Scene.LobbyPlayScene);
        });
        joinGameButton.onClick.AddListener(() => { 
            NetworkManager.Singleton.StartClient();
        });
    }
}
