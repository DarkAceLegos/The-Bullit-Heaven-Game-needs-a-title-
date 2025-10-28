using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive (false);
            hasDataContent.SetActive (true);

            nameText.text = "Coins Count: " + data.coins;
        }
    }

    public string GetProfileId()
    { return this.profileId; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            DataPersistenceManager.Instance.ChangeSelectedProfileId(profileId);

            if (!SaveSlotMenuUi.Instance.IsGameLoading())
            {
                DataPersistenceManager.Instance.NewGame();
            }

            Loader.Load(Loader.Scene.LobbyScene);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right Click");
    }
}
