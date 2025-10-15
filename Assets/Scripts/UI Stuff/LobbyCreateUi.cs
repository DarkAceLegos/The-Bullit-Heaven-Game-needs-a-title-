using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUi : MonoBehaviour
{
    [SerializeField] private TMP_InputField lobbyNameInputFeild;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        createPrivateButton.onClick.AddListener(() =>
        {
            GameLobby.instance.CreateLobby(lobbyNameInputFeild.text, true);
        });
        createPublicButton.onClick.AddListener(() =>
        {
            GameLobby.instance.CreateLobby(lobbyNameInputFeild.text, false);
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
