using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUi : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinLobbyButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private LobbyCreateUi LobbyCreateUi;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => 
        { 
            GameLobby.instance.LeaveLobby();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        createLobbyButton.onClick.AddListener(() =>
        {
            LobbyCreateUi.Show();
        });
        quickJoinLobbyButton.onClick.AddListener(() =>
        {
            GameLobby.instance.QuickJoin();
        });
        joinCodeButton.onClick.AddListener(() =>
        {
            GameLobby.instance.JoinWithCode(joinCodeInputField.text);
        });
    }

    private void Start()
    {
        playerNameInputField.text = GameMultiplayerConnectionAppoval.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            GameMultiplayerConnectionAppoval.Instance.SetPlayerName(newText);
        });
    }
}
