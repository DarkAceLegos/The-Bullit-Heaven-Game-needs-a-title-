using UnityEngine;
using UnityEngine.UI;

public class CharacterColorSelectUi : MonoBehaviour
{
    [SerializeField] private int colorId;
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedGameObject;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            GameMultiplayerConnectionAppoval.Instance.ChangePlayerColor(colorId);
        });
    }

    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnPlayerDataNetworkChanged += GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged;
        image.color = GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(colorId);
        UpdateIsSelected();
    }

    private void GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged(object sender, System.EventArgs e)
    {
        UpdateIsSelected();
    }

    private void UpdateIsSelected()
    {
        if (GameMultiplayerConnectionAppoval.Instance.GetPlayerData().colorId == colorId)
        {
            selectedGameObject.SetActive(true);
        }
        else
        {
            selectedGameObject.SetActive(false);
        }
    }
}
