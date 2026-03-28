using UnityEngine;
using UnityEngine.UI;

public class DeckClickAttack : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MetaProgressionUi ui;

    private void Awake()
    {
        button.onClick.AddListener(() => { 
            ui.Hide(); 
            ui.GetComponent<DeckChouse>().unlocked = true;
        });
    }
}
