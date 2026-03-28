using UnityEngine;
using UnityEngine.UI;

public class MetaProgressionUi : MonoBehaviour
{
    public static MetaProgressionUi Instance { get; private set; }

    [SerializeField] private Button closeButton;
    [SerializeField] private bool hideOnStartNot;

    private void Awake()
    {
        //Instance = this;

        closeButton.onClick.AddListener(() => { Hide(); });
    }

    private void Start()
    {
        if (!hideOnStartNot)
        { Hide(); }
        else
        { Show(); }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
