using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
    public static GameOverUi Instance { get; private set; }

    [SerializeField] TextMeshProUGUI stuffText;
    [SerializeField] Button playAgainButton;

    private void Awake()
    {
        Instance = this;
        playAgainButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
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
