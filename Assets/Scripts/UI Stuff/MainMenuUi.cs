using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button playButtonMulti;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level);
        });
        playButtonMulti.onClick.AddListener(() =>
        {

        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        optionButton.onClick.AddListener(() =>
        {

        });

        Time.timeScale = 1.0f;
    }
}
