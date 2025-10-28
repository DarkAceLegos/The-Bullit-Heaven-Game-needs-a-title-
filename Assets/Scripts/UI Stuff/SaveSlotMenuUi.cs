using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotMenuUi : MonoBehaviour
{
    public static SaveSlotMenuUi Instance { get; private set; }

    private SaveSlot[] saveSlots;

    [SerializeField] private Button closeButton;

    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();

        Instance = this;

        closeButton.onClick.AddListener(() => { Hide(); });
    }

    private void Start()
    {
        Hide();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profileGameData = DataPersistenceManager.Instance.GetAllProfileGaemData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profileGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.gameObject.SetActive(false);
            }
            else 
            {
                saveSlot.gameObject.SetActive(true);
            }
        }
    }

    public void Show(bool isLoadingGame)
    {
        gameObject.SetActive(true);
        ActivateMenu(isLoadingGame);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


    public bool IsGameLoading() {  return isLoadingGame; }
}
