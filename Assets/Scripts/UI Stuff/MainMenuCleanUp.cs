using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanUp : MonoBehaviour
{
    private void Awake()
    {
        if(NetworkManager.Singleton != null)
            Destroy(NetworkManager.Singleton.gameObject);
        if(GameMultiplayerConnectionAppoval.Instance != null)
            Destroy(GameMultiplayerConnectionAppoval.Instance.gameObject);
    }
}
