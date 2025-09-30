using UnityEngine;

public class GameLost : GameBaseState
{
    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("Game lost");
    }

    public override void ExitState(GameStateManager gameState)
    {
        Debug.Log("exit game lost");
    }

    public override void UpdateState(GameStateManager gameState)
    {
        
    }
}
