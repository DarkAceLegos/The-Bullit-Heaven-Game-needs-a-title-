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
            GameMultiplayerConnectionAppoval.Instance.StartHost();
            Loader.LoadNetwork(Loader.Scene.LobbyPlayScene);
        });
        joinGameButton.onClick.AddListener(() => {
            GameMultiplayerConnectionAppoval.Instance.StartClient();
        });
    }
}
