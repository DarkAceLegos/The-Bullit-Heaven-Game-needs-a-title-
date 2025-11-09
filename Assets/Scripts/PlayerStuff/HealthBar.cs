using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Player Player;

    public static HealthBar Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthBar.maxValue = Player.GetComponent<PlayerHealth>().GetMaxHeath();
        healthBar.value = Player.GetComponent<PlayerHealth>().GetCurrentHeath();
    }

    public void HealthChange(int health)
    { healthBar.value = health;}
}
