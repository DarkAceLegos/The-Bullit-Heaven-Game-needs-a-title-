using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private PlayerHealth Player;

    public static HealthBar Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthBar.maxValue = Player.GetMaxHeath();
        healthBar.value = Player.GetCurrentHeath();
    }

    public void HealthChange(int health)
    {
        healthBar.maxValue = Player.GetMaxHeath(); // need to find better spot
        healthBar.value = health;
    }
}
