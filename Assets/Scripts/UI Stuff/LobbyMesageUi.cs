using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMesageUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnFailedToJoinGame += GameMultiplayerConnectionAppoval_OnFailedToJoinGame;
        GameLobby.instance.OnCreateLobbyStarted += GameLobby_OnCreateLobbyStarted;
        GameLobby.instance.OnCreateLobbyFailed += GameLobby_OnCreateLobbyFailed;
        GameLobby.instance.OnJoinStarted += GameLobby_OnJoinStarted;
        GameLobby.instance.OnQuickJoinFailed += GameLobby_OnQuickJoinFailed;
        GameLobby.instance.OnCodeJoinFailed += GameLobby_OnCodeJoinFailed;
        Hide();
    }

    private void GameLobby_OnCodeJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to join Lobby!");
    }

    private void GameLobby_OnQuickJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Could not find a Lobby to Quick Join!");
    }

    private void GameLobby_OnJoinStarted(object sender, EventArgs e)
    {
        ShowMessage("Join Lobby...");
    }

    private void GameLobby_OnCreateLobbyFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to create Lobby!");
    }

    private void GameLobby_OnCreateLobbyStarted(object sender, EventArgs e)
    {
        ShowMessage("Create Lobby...");
    }

    private void GameMultiplayerConnectionAppoval_OnFailedToJoinGame(object sender, EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connect");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
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
        GameMultiplayerConnectionAppoval.Instance.OnFailedToJoinGame -= GameMultiplayerConnectionAppoval_OnFailedToJoinGame;
    }
}
