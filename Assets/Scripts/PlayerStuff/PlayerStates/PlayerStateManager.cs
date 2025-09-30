using Unity.Netcode;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;

    public PlayerAlive Alive = new PlayerAlive();
    public PlayerDead Dead = new PlayerDead();

    public PlayerBaseState GetCurrentState() { return currentState; }

    void Start()
    {
        currentState = Alive;

        //Debug.Log(this);

        currentState.EnterState(this);

        //Debug.Log(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    //[Rpc(SendTo.ClientsAndHost)]
    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
