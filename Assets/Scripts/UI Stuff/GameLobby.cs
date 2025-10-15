using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GameLobby : MonoBehaviour
{
    public static GameLobby instance { get; private set; }

    private void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthtication();
    }

    private async void InitializeUnityAuthtication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions InitializationOptions = new InitializationOptions();
            InitializationOptions.SetProfile(Random.Range(0, 10000).ToString());

            await UnityServices.InitializeAsync(InitializationOptions);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public void CreateLobby(string lobbyName, bool isPrivate)
    {

    }
}
