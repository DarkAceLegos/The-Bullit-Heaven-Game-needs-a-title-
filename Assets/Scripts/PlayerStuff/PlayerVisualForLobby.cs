using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisualForLobby : MonoBehaviour
{
    [SerializeField] int playerIndex = 0;
    [SerializeField] private GameObject ReadyText;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickButton;

    private void Awake()
    {
        kickButton.onClick.AddListener(() =>
        {
            PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            GameMultiplayerConnectionAppoval.Instance.KickPlayer(playerData.clientId);
        });
    }

    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnPlayerDataNetworkChanged += GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged;

        CharaterReady.instance.OnReadyChanged += CharaterReady_OnReadyChanged;

        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharaterReady_OnReadyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if (GameMultiplayerConnectionAppoval.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromPlayerIndex(playerIndex);

            ReadyText.SetActive(CharaterReady.instance.IsPlayerReady(playerData.clientId));

            playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));
        }
        else
        {
            Hide();
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
}
