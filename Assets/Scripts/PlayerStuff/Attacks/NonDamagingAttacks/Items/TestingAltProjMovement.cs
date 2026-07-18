using Unity.Netcode;
using UnityEngine;

public class TestingAltProjMovement : Attack
{
    public override void Tick(NetworkObject player, int Direction = 0)
    {
        return;
    }

    protected override void OnInitialize()
    {
        
    }
}
