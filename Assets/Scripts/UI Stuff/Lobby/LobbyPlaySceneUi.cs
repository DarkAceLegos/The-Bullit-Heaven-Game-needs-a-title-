using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlaySceneUi : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private Button metaProgressionButton;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            GameLobby.instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        readyButton.onClick.AddListener(() => {
            CharaterReady.instance.SetPlayerReady();
        });
        metaProgressionButton.onClick.AddListener(() => { MetaProgressionUi.Instance.Show(); });
    }

    private void Start()
    {
        Lobby lobby = GameLobby.instance.GetLobby();

        if (!GameMultiplayerConnectionAppoval.playMultiplyer)
        {
            lobbyNameText.text = "Single Player";
            lobbyCodeText.text = "";
        }
        else
        {
            lobbyNameText.text = "Lobby Name: " + lobby.Name;
            lobbyCodeText.text = "Lobby Code: " + lobby.LobbyCode;
        }
    }
}
