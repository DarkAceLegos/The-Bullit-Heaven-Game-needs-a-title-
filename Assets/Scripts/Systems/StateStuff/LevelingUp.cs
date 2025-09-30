using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class LevelingUp : GameBaseState
{
    [SerializeField] private List<AttackData> allAttacks = new();

    private GameStateManager _gameStateManager;

    [Header("UI")]
    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private Transform upgradeHolder;
    [SerializeField] private LevelEntry entryPrefab;

    private List<ulong> readyPlayers = new();

    public override void EnterState(GameStateManager gameState)
    {
        //Debug.Log("entered Leveling Up");
        _gameStateManager = gameState;
        readyPlayers.Clear();

        SetUpLevelOptions();
        levelScreen.SetActive(true);
        waitingScreen.SetActive(false);
        Time.timeScale = 0;
    }

    public override void ExitState(GameStateManager gameState)
    {
        levelScreen.SetActive(false);
        waitingScreen.SetActive(false );
        Debug.Log("exit leveling up");
        Time.timeScale = 1;
    }

    public override void UpdateState(GameStateManager gameState)
    {
        
    }

    private void SetUpLevelOptions()
    {
        foreach (Transform child in upgradeHolder)
            UnityEngine.Object.Destroy(child.gameObject);

        List<AttackData> availableAttacks = GetAvailableAttacks();
        if (availableAttacks==null || availableAttacks.Count <= 0)
        {
            Debug.Log("no available attacks for level up");
            return;
        }

        var randomAttack = new List<AttackData>();
        while (randomAttack.Count < 3 && availableAttacks.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableAttacks.Count);
            randomAttack.Add(availableAttacks[randomIndex]);
            availableAttacks.RemoveAt(randomIndex);
        }

        foreach (var attack in randomAttack)
        {
            var entry = UnityEngine.Object.Instantiate(entryPrefab, upgradeHolder);
            entry.Init(attack, this);//*/
        }
    }

    private List<AttackData> GetAvailableAttacks()
    {
        if(AttackHandler.LoaclInstance == null) return null;

        List<AttackData> availableAttacks = new List<AttackData>();
        foreach(var attack in allAttacks)
        {
            if(AttackHandler.LoaclInstance.getLevel(attack.attackId)<attack.maxLevel)
                availableAttacks.Add(attack);
        }

        return availableAttacks;
    }

    internal void SetReady()
    {
        waitingScreen.SetActive(true);
        SetReadyRpc(Player.LoaclInstance.GetPlayerId());
    }

    [Rpc(SendTo.Server)]
    private void SetReadyRpc(ulong playerId)
    {
        if(readyPlayers.Contains(playerId)) return;

        readyPlayers.Add(playerId);

        if (readyPlayers.Count < PlayerHealth._allPlayers.Count) return;

        _gameStateManager.SwitchState(_gameStateManager.GameRunning);
    }
}
