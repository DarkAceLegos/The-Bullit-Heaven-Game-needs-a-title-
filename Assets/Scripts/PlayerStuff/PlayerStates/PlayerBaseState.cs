using System;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class PlayerBaseState 
{
    public abstract void EnterState(PlayerStateManager playerState);

    public abstract void UpdateState(PlayerStateManager playerState);

    public abstract void ExitState(PlayerStateManager playerState);
}
