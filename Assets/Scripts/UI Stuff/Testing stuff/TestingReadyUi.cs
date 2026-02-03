using UnityEngine;
using UnityEngine.UI;

public class TestingReadyUi : MonoBehaviour
{
    [SerializeField] private Button readyButton;

    private void Awake()
    {
        readyButton.onClick.AddListener(() =>
        {
            CharaterReady.instance.SetPlayerReady();
        });
    }
}
