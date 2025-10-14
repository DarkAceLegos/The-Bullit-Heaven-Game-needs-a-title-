using System;
using UnityEngine;

public class ConnetingUi : MonoBehaviour
{
    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnTryingToJoinGame += GameMultiplayerConnectionAppoval_OnTryingToJoinGame;
        GameMultiplayerConnectionAppoval.Instance.OnFailedToJoinGame += GameMultiplayerConnectionAppoval_OnFailedToJoinGame;
        Hide();
    }

    private void GameMultiplayerConnectionAppoval_OnFailedToJoinGame(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameMultiplayerConnectionAppoval_OnTryingToJoinGame(object sender, EventArgs e)
    {
        Show();
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
        GameMultiplayerConnectionAppoval.Instance.OnTryingToJoinGame -= GameMultiplayerConnectionAppoval_OnTryingToJoinGame;
        GameMultiplayerConnectionAppoval.Instance.OnFailedToJoinGame -= GameMultiplayerConnectionAppoval_OnFailedToJoinGame;
    }
}
