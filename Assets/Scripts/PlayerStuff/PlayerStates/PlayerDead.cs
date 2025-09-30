using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerDead : PlayerBaseState
{
    //[SerializeField] private GameObject graphicsDead;
    private float playerMoveSpeed;
    private AttackHandler attackHandler;
    private float reviveDistance = 2f;
    private float reviveTime = 3f;

    private float reviveProgress;

    public override void EnterState(PlayerStateManager playerState)
    {
        Debug.Log("entered the dead State");

        playerState.TryGetComponent<Player>(out Player player);

        //Debug.Log(player);

        player.TryGetComponent<AttackHandler>(out attackHandler);

        //Debug.Log(attackHandler);

        attackHandler.enabled = false;
        
        playerMoveSpeed = player.moveSpeed;
        player.moveSpeed = 0;

        //graphicsDead.SetActive(true);
    }

    public override void ExitState(PlayerStateManager playerState)
    {
        //graphicsDead?.SetActive(false);
        attackHandler.enabled = true;
        playerState.TryGetComponent<Player>(out Player player);
        player.moveSpeed = playerMoveSpeed;
    }

    public override void UpdateState(PlayerStateManager playerState)
    {
        bool beingRevived = false;
        foreach (var player in PlayerHealth._allPlayers.Values)
        { 
            if(player.IsOwner) continue;

            if (Vector2.Distance(player.transform.position, playerState.transform.position) > reviveDistance)
                continue;

            beingRevived = true;
            reviveProgress += Time.deltaTime;
        }

        //Debug.Log("trying to revive in");

        if (!beingRevived) 
        {
            reviveProgress = 0; 
        }

        if (reviveProgress >= reviveTime)
        {
            Debug.Log("revived");

            reviveProgress = 0;

            playerState.SwitchState(playerState.Alive); 
        }
    }
}
