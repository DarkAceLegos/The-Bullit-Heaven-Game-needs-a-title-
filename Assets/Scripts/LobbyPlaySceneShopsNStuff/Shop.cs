using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private MetaProgressionUi shopUi;
    [SerializeField] private Button shopButton;

    private void Awake()
    {
        shopButton.onClick.AddListener(() =>
        {
            shopUi.Show();
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameInputs.Instance.OnInteractAction += Instance_OnInteractAction;
    }

    private void Instance_OnInteractAction(object sender, System.EventArgs e)
    {
        shopUi.Show();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameInputs.Instance.OnInteractAction -= Instance_OnInteractAction;
    }
}
