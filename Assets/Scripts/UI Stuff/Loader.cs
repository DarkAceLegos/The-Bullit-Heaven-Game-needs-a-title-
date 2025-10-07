using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{

    private static Scene targetScene;

    public enum Scene
    {
        MainMenuScene,
        Lobby,
        Level,
        LoadingScene
    }


    public static void Load(Scene targetSceen)
    {
        Loader.targetScene = targetSceen;

        SceneManager.LoadScene(Loader.)

        SceneManager.LoadScene(targetSceen.ToString());
    }

}
