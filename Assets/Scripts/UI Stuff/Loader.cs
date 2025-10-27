using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class Loader 
{

    private static Scene targetScene;

    public enum Scene
    {
        MainMenuScene,
        LobbyScene,
        LevelScene,
        LoadingScene,
        LobbyPlayScene
    }


    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        InputSystem.actions.Enable();

        //DataPersistenceManager.Instance.SaveGame();

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadNetwork(Scene targetScene)
    {
        InputSystem.actions.Enable();

        //DataPersistenceManager.Instance.SaveGame();

        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
    }


    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
