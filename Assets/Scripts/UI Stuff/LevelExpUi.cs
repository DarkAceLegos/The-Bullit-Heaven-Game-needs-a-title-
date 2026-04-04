using UnityEngine;
using UnityEngine.UI;

public class LevelExpUi : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    [SerializeField] private LevelManager levelManager;

    public static LevelExpUi instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        expBar.maxValue = levelManager.expToNextLevel;
        expBar.value = levelManager.experiance;
    }

    public void ExpChange(float exp)
    {
        expBar.maxValue = levelManager.expToNextLevel;
        expBar.value = exp;
    }
}
