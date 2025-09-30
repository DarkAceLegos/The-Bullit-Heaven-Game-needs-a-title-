using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    private static readonly Dictionary<ulong, PlayerHealth> allPlayers = new();
    public static Dictionary<ulong, PlayerHealth> _allPlayers => allPlayers;

    public event EventHandler<OnHeathChangeEventArgs> OnHeathChange;
    public class OnHeathChangeEventArgs :EventArgs
    {
        public int heathChange;
    }

    public event EventHandler<OnPlayerDied> onPlayerDied_Local;

    public class OnPlayerDied : EventArgs { public PlayerStateManager playerStateManager; }

    public static Action<ulong> onPlayerDied;

    [SerializeField] private int maxHeath = 100;
    [SerializeField] private int currentHeath = 100;
    private ulong playerId;

    private PlayerStateManager playerStateManager;

    public int GetMaxHeath() {  return maxHeath; }
    public int GetCurrentHeath() { return currentHeath; }
    public bool isDowned => currentHeath <= 0;

    [RuntimeInitializeOnLoadMethod]
    private static void Clear() {  allPlayers.Clear(); }

    private void Awake()
    {
        Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
    }

    private void Start()
    {
        Player.LoaclInstance.TryGetComponent<PlayerStateManager>(out PlayerStateManager playerState);

        Debug.Log(playerState);

        playerStateManager = playerState;
    }

    private void Player_OnAnyPlayerSpawned(object sender, Player.OnAnyPlayerSpawnedEventArgs e)
    {
        AddPlayerHealthToThePlayerHealthListAllPlayersRpc(e.clientId);
        playerId = e.clientId;
    }

    [Rpc(SendTo.Server)]
    private void AddPlayerHealthToThePlayerHealthListAllPlayersRpc(ulong playerId)
    {
        allPlayers[playerId] = this;

        //Debug.Log("added " + playerId + " to the list");
    }

    public void changeHealth(int heathChange)
    {
        if(!IsOwner) return;

        currentHeath = Mathf.Clamp(currentHeath + heathChange, 0, maxHeath);

        OnHeathChange?.Invoke(this, new OnHeathChangeEventArgs
        {
            heathChange = currentHeath, //the event to have health bar lisin to
        });

        ShowHeathChangeRpc(currentHeath);

        if (currentHeath == 0) 
        {
            Die();
        }//*/
    }

    private void Die()
    {
        onPlayerDied_Local?.Invoke(this, new OnPlayerDied
        {
            playerStateManager = playerStateManager
        });

        Debug.Log("You are down");
    }

    public void setHeath(int newHeath)
    {
        if(!IsOwner) { return; }

        currentHeath = newHeath;

        OnHeathChange?.Invoke(this, new OnHeathChangeEventArgs
        {
            heathChange = newHeath
        });

        ShowHeathChangeRpc(currentHeath);
    }

    [Rpc(SendTo.Everyone)]
    private void ShowHeathChangeRpc(int heath)
    {
        currentHeath = heath;

        if (currentHeath == 0)
        {
            onPlayerDied?.Invoke(playerId); //might not wo
        }
    }

    public override void OnDestroy()
    {
        Player.OnAnyPlayerSpawned -= Player_OnAnyPlayerSpawned;
    }
}
