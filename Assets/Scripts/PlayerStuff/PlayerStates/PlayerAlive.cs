using System;
using UnityEngine;

public class PlayerAlive : PlayerBaseState
{
    //graphics for rightnow will be changed latter
    //[SerializeField] private GameObject graphics;
    [SerializeField] private PlayerHealth playerHealth;

    public override void EnterState(PlayerStateManager playerState)
    {
        Debug.Log("entered the alive state");

        playerState.TryGetComponent<Player>(out Player player);

        //Debug.Log(player);
        
        player.TryGetComponent<PlayerHealth>(out playerHealth);

        //Debug.Log($"PlayerHealth: {playerHealth}");

        playerHealth.onPlayerDied_Local += PlayerHealth_OnPlayerDied;

        //graphics.SetActive(true);

       playerHealth.setHeath(playerHealth.GetMaxHeath());
    }

    private void PlayerHealth_OnPlayerDied(object sender, PlayerHealth.OnPlayerDied e)
    {
        e.playerStateManager.SwitchState(e.playerStateManager.Dead);
    }

    public override void ExitState(PlayerStateManager playerState)
    {
        //graphics?.SetActive(false);
        playerHealth.onPlayerDied_Local -= PlayerHealth_OnPlayerDied;
    }

    public override void UpdateState(PlayerStateManager playerState)
    {
    
    }
}
