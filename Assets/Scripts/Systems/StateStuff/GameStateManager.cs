using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameBaseState currentState;

    public WaitingForPlayers WaitingForPlayers = new WaitingForPlayers();
    public GameRunning GameRunning = new GameRunning();
    public LevelingUp LevelingUp = new LevelingUp();
    public GameLost GameLost = new GameLost();

    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private Transform upgradeHolder;
    [SerializeField] private LevelEntry entryPrefab;

    public GameObject GetLevelScreen() {  return levelScreen; }
    public GameObject GetWaitingScreen() { return waitingScreen; }
    public Transform GetUpgradeHolder() { return upgradeHolder; }
    public LevelEntry GetLevelEntry() { return entryPrefab; }

    public GameBaseState GetCurrentGameState() { return currentState; }

    void Start()
    {
        currentState = WaitingForPlayers;

        //Debug.Log(this);

        currentState.EnterState(this);

        //Debug.Log(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
