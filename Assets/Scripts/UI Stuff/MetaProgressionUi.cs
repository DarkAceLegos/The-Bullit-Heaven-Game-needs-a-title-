using UnityEngine;
using UnityEngine.UI;

public class MetaProgressionUi : MonoBehaviour
{
    public static MetaProgressionUi Instance { get; private set; }

    [SerializeField] private Button closeButton;

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() => { Hide(); });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
