using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class LevelingUp : GameBaseState
{
    [SerializeField] private List<AttackData> allAttacks = new();

    [SerializeField] private List<AttackData> noOtherAttacks = new();

    private GameStateManager _gameStateManager;

    private GameObject levelScreen;
    private GameObject waitingScreen;
    private Transform upgradeHolder;
    private LevelEntry entryPrefab;

    public override void EnterState(GameStateManager gameState)
    {
        allAttacks = Player.LoaclInstance.GetAllPlayerUnlockedAttacks();

        noOtherAttacks = Player.LoaclInstance.GetPlayerNoOtherAttacks();

        //Debug.Log("entered Leveling Up");
        _gameStateManager = gameState;
        //readyPlayers.Clear();

        levelScreen = gameState.GetLevelScreen();
        waitingScreen = gameState.GetWaitingScreen();
        upgradeHolder = gameState.GetUpgradeHolder();
        entryPrefab = gameState.GetLevelEntry();

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

        Debug.Log("Upgrade Holder is clear");

        List<AttackData> availableAttacks = GetAvailableAttacks();
        if (availableAttacks == null || availableAttacks.Count <= 0)
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

        if (randomAttack.Count < 3 && noOtherAttacks.Count > 0) 
        {
            while (noOtherAttacks.Count < 3 && noOtherAttacks.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, noOtherAttacks.Count);
                randomAttack.Add(noOtherAttacks[randomIndex]);
                //noOtherAttacks.RemoveAt(randomIndex);
            }
        }

        foreach (var attack in randomAttack)
        {
            var entry = UnityEngine.Object.Instantiate(entryPrefab, upgradeHolder);
            entry.Init(attack, this);//*/
        }
    }

    private List<AttackData> GetAvailableAttacks()
    {
        if (AttackHandler.LoaclInstance == null) { Debug.Log("the Attack Handler is null"); return null; }

        List<AttackData> availableAttacks = new List<AttackData>();
        foreach(var attack in allAttacks)
        {
            if (AttackHandler.LoaclInstance.getLevel(attack.attackId) < attack.maxLevel)
                availableAttacks.Add(attack);
        }

        return availableAttacks;
    }

    internal void SetReady()
    {
        waitingScreen.SetActive(true);

        Debug.Log("trying to send Rpc");

        _gameStateManager.GSMSetReadyRpc(Player.LoaclInstance.OwnerClientId);
    }

    public void SetReadyRpc()
    {
        _gameStateManager.SwitchState(_gameStateManager.GameRunning);
    }
}
