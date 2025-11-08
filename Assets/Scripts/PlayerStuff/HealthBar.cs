using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public static HealthBar Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthBar.maxValue = Player.LoaclInstance.GetComponent<PlayerHealth>().GetMaxHeath();
        healthBar.value = Player.LoaclInstance.GetComponent<PlayerHealth>().GetCurrentHeath();
    }

    public void HealthChange(int health)
    { healthBar.value = health;}
}
