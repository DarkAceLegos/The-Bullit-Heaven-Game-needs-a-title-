using System;
using UnityEngine;

public class GameRunning : GameBaseState
{

    private GameStateManager _gameStateManager;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("entered game running");

        _gameStateManager = gameState;

        //if(!IsServer) { return; }

        PlayerHealth.onPlayerDied += OnPlayerDeid;
        LevelManager.Instance.OnLevelChange += LevelManager_OnLevelChange;
    }

    private void OnPlayerDeid(ulong playerId)
    {
        Debug.Log("player died");

        foreach (var player in PlayerHealth._allPlayers)
        {
            Debug.Log("checking if all players are down");

            if (!player.Value.isDowned)
                return;

            _gameStateManager?.SwitchState(_gameStateManager.GameLost);
        }
    }

    private void LevelManager_OnLevelChange(object sender, LevelManager.OnLevelChangeEventArgs e)
    {
        _gameStateManager?.SwitchState(_gameStateManager.LevelingUp);
    }

    public override void ExitState(GameStateManager gameState)
    {
        PlayerHealth.onPlayerDied -= OnPlayerDeid;
        LevelManager.Instance.OnLevelChange -= LevelManager_OnLevelChange;

        Debug.Log("exit game running");
    }

    public override void UpdateState(GameStateManager gameState)
    {
        
    }
}
