using UnityEngine;
using UnityEngine.UI;

public class SaveSystemSceneUi : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(() =>
        {
            SaveSlotMenuUi.Instance.Show(false);

            //DataPersistenceManager.Instance.NewGame();
            //Loader.Load(Loader.Scene.LobbyScene);
        });
        continueGameButton.onClick.AddListener(() =>
        {
            if (!DataPersistenceManager.Instance.HasGameData())
            {
                //show error screen
                return;
            }
            Loader.Load(Loader.Scene.LobbyScene);
        });
        loadGameButton.onClick.AddListener(() =>
        {
            SaveSlotMenuUi.Instance.Show(true);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        //Time.timeScale = 1.0f;
    }

    private void Start()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.gameObject.SetActive(false);
            loadGameButton.gameObject.SetActive(false);
            return;
        }        
    }
}
