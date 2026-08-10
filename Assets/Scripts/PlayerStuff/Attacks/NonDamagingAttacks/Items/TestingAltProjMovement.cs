using Unity.Netcode;
using UnityEngine;

public class TestingAltProjMovement : Attack
{
    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
    {
        return;
    }

    protected override void OnInitialize()
    {
        
    }
}
