using UnityEngine;
using UnityEngine.UI;

public class StatCompBandatForSkillTree : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform altClose;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => { Hide(); });
    }

    public void Show()
    {
        altClose.gameObject.SetActive(true);
    }

    public void Hide()
    {
        altClose.gameObject.SetActive(false);
    }
}
