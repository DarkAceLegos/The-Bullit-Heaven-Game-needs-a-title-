using UnityEngine;
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
            GameMultiplayerConnectionAppoval.playMultiplyer = false;
            Loader.Load(Loader.Scene.SaveSystemScene);
        });
        playButtonMulti.onClick.AddListener(() =>
        {
            GameMultiplayerConnectionAppoval.playMultiplyer = true;
            Loader.Load(Loader.Scene.SaveSystemScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        optionButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });

        Time.timeScale = 1.0f;
    }
}
