using UnityEngine;

public class WaitingForPlayers : GameBaseState
{
    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("entered waiting for players");
        gameState.SwitchState(gameState.GameRunning);
    }

    public override void ExitState(GameStateManager gameState)
    {
        Debug.Log("exit waiting for players");
    }

    public override void UpdateState(GameStateManager gameState)
    {
        
    }
}
