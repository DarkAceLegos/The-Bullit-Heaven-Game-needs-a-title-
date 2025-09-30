
using UnityEngine;

public abstract class GameBaseState //: NetworkBehaviour
{
    public abstract void EnterState(GameStateManager gameState);

    public abstract void UpdateState(GameStateManager gameState);

    public abstract void ExitState(GameStateManager gameState);
}
