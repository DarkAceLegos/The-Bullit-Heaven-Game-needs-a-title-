using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameBaseState currentState;

    public WaitingForPlayers WaitingForPlayers = new WaitingForPlayers();
    public GameRunning GameRunning = new GameRunning();
    public LevelingUp LevelingUp = new LevelingUp();
    public GameLost GameLost = new GameLost();

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
