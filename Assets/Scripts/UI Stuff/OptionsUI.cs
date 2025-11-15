using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Button moveUpButtom;
    [SerializeField] private Button moveDownButtom;
    [SerializeField] private Button moveLeftButtom;
    [SerializeField] private Button moveRightButtom;
    [SerializeField] private Button interactButtom;
    [SerializeField] private Button pauseButtom;

    [SerializeField] Transform pressToRebindTransform;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            //MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        backButton.onClick.AddListener(() => 
        {
            Hide();
        });
        pauseButtom.onClick.AddListener(() => 
        { 
            RebindBinding(GameInputs.Binding.Pause);
        });
        interactButtom.onClick.AddListener(() => { RebindBinding(GameInputs.Binding.Interact); });
        moveUpButtom.onClick.AddListener(() => { PlayerMoveRebindBinding(Player.Binding.Up); });
        moveDownButtom.onClick.AddListener(() => { PlayerMoveRebindBinding(Player.Binding.Down); });
        moveRightButtom.onClick.AddListener(() => { PlayerMoveRebindBinding(Player.Binding.Right); });
        moveLeftButtom.onClick.AddListener(() => { PlayerMoveRebindBinding(Player.Binding.Left); });
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == Loader.Scene.LevelScene.ToString())
        { GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused; }

        //UpdateVisual();

        Hide();

        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        //soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        //musicText.text = "Muisc: " + Mathf.Round(MuiscManager.Instance.GetVolume() * 10f);

        moveUpText.text = Player.LoaclInstance.GetBindingText (Player.Binding.Up);
        moveDownText.text = Player.LoaclInstance.GetBindingText (Player.Binding.Down);
        moveRightText.text = Player.LoaclInstance.GetBindingText (Player.Binding.Right);
        moveLeftText.text = Player.LoaclInstance.GetBindingText (Player.Binding.Left);
        interactText.text = GameInputs.Instance.GetBindingText(GameInputs.Binding.Interact);
        pauseText.text = GameInputs.Instance.GetBindingText (GameInputs.Binding.Pause);
    }

    public void Show()
    {
        //Debug.Log("trying to show options");

        gameObject.SetActive(true);
        UpdateVisual();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    { pressToRebindTransform.gameObject.SetActive(true); }

    private void HidePressToRebindKey()
    { pressToRebindTransform.gameObject.SetActive(false); }

    private void RebindBinding(GameInputs.Binding binding)
    {
        ShowPressToRebindKey();
        GameInputs.Instance.RebindBinding (binding, () => 
        {
            HidePressToRebindKey();
            UpdateVisual();            
        });
    }

    private void PlayerMoveRebindBinding(Player.Binding binding)
    {
        ShowPressToRebindKey();
        Player.LoaclInstance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
