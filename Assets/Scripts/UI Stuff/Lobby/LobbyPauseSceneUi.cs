using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPauseSceneUi : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    private bool on;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            GameLobby.instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    private void Start()
    {
        GameInputs.Instance.OnPauseAction += Instance_OnPauseAction;

        Hide();
    }

    private void Instance_OnPauseAction(object sender, EventArgs e)
    {
        if(on) Hide();
        else Show();
    }

    private void Show()
    {
        Debug.Log("pause");
        gameObject.SetActive(true);
        on = true;
    }

    private void Hide()
    {
        Debug.Log("Unpause");
        gameObject.SetActive(false);
        on = false;
    }
}
