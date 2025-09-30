using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameStateManager : NetworkBehaviour
{
    public GameBaseState currentState;

    public WaitingForPlayers WaitingForPlayers = new WaitingForPlayers();
    public GameRunning GameRunning = new GameRunning();
    public LevelingUp LevelingUp = new LevelingUp();
    public GameLost GameLost = new GameLost();

    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private Transform upgradeHolder;
    [SerializeField] private LevelEntry entryPrefab;

    private List<ulong> readyPlayers = new();

    public GameObject GetLevelScreen() {  return levelScreen; }
    public GameObject GetWaitingScreen() { return waitingScreen; }
    public Transform GetUpgradeHolder() { return upgradeHolder; }
    public LevelEntry GetLevelEntry() { return entryPrefab; }

    public GameBaseState GetCurrentGameState() { return currentState; }

    void Start()
    {
        currentState = WaitingForPlayers;

        //Debug.Log(this);

        currentState.EnterState(this);

        //Debug.Log(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        readyPlayers.Clear();
        currentState = state;
        Debug.Log($"changing state to {state}");
        state.EnterState(this);
    }

    [Rpc(SendTo.Server)]
    public void GSMSetReadyRpc(ulong playerId)
    {
        if (readyPlayers.Contains(playerId)) return;

        readyPlayers.Add(playerId);

        Debug.Log($"I am ready {playerId}");

        if (readyPlayers.Count < PlayerHealth._allPlayers.Count) return;

        Debug.Log($"everbody is ready {playerId}");

        TellEveryOneWeAreDownLevelingUpRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void TellEveryOneWeAreDownLevelingUpRpc()
    {
        LevelingUp.SetReadyRpc();
    }
}
