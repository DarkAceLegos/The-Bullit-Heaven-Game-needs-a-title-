using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Matchmaker.Models;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    [SerializeField] public PlayerMetaProgression playerMetas;

    public static event EventHandler<OnAnyPlayerSpawnedEventArgs> OnAnyPlayerSpawned;
    public class OnAnyPlayerSpawnedEventArgs : EventArgs
    {
        public NetworkObject player;
        public ulong clientId;
    }

    public static Player LoaclInstance { get; private set; }

    public float moveSpeed;
    [SerializeField] public int additiveMaxHealthModifier;
    [SerializeField] public float percentageMaxHealthModifier = 1f;
    [SerializeField] public int additiveDamageModifier;
    [SerializeField] public float percentageDamageModifier = 1f;
    [SerializeField] public float percentageCooldownModifier = 1f;
    [SerializeField] public int additiveProjectileModifier;
    [SerializeField] public int additiveAreaModifier;
    [SerializeField] public float percentageAreaModifier = 1f;
    [SerializeField] public float enemySpawnModifier;
    [SerializeField] public float enemyDamageModifier;
    [SerializeField] public int playerHealthRegen;
    [SerializeField] public float percentagePlayerHealthRegen = 1f;
    [SerializeField] public float percentageTreasureFind = 1f;
    [SerializeField] public float percentageTreasurGain = 1f;
    [SerializeField] public int additivePlayerMoveSpeed;
    [SerializeField] public float percentagePlayerMoveSpeed = 1f;
    [SerializeField] public int additiveProjectileSpeed;
    [SerializeField] public float percentageProjectileSpeed = 1f;
    [SerializeField] public int additiveDuration;
    [SerializeField] public float percentageDuration = 1f;
    [SerializeField] public int additiveExperience;
    [SerializeField] public float percentageExperience = 1f;

    [SerializeField] private Rigidbody2D rb;

    InputAction moveAction;

    [SerializeField] private CinemachineCamera vc;
    [SerializeField] private AudioListener listener;
    [SerializeField] PlayerVisual playerVisual;

    [SerializeField] private List<AttackData> allAttacksPlayerUnlocked = new();

    [SerializeField] private List<AttackData> noOtherAttacks = new();

    public List<AttackData> GetAllPlayerUnlockedAttacks() { return allAttacksPlayerUnlocked; }
    public List<AttackData> GetPlayerNoOtherAttacks() { return noOtherAttacks; }

    private void Awake()
    {
        
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));

        allAttacksPlayerUnlocked = playerMetas.allAttacksPlayerUnlocked;

        //Debug.Log(moveAction.GetBindingDisplayString(1));
        //Debug.Log(GetBindingText(Binding.Up));


        additiveMaxHealthModifier = playerMetas.additiveMaxHealthModifier;
        percentageMaxHealthModifier = playerMetas.percentageMaxHealthModifier;
        additiveDamageModifier = playerMetas.additiveDamageModifier;
        percentageDamageModifier = playerMetas.percentageDamageModifier;
        percentageCooldownModifier = playerMetas.percentageCooldownModifier;
        additiveProjectileModifier = playerMetas.additiveProjectileModifier;
        additiveAreaModifier = playerMetas.additiveAreaModifier;
        percentageAreaModifier = playerMetas.percentageAreaModifier;
        enemySpawnModifier = playerMetas.enemySpawnModifier;
        enemyDamageModifier = playerMetas.enemyDamageModifier;
        playerHealthRegen = playerMetas.playerHealthRegen;
        percentagePlayerHealthRegen = playerMetas.percentagePlayerHealthRegen;
        percentageTreasureFind = playerMetas.percentageTreasureFind;
        percentageTreasurGain = playerMetas.percentageTreasurGain;
        additivePlayerMoveSpeed = playerMetas.additivePlayerMoveSpeed;
        percentagePlayerMoveSpeed = playerMetas.percentagePlayerMoveSpeed;
        additiveProjectileSpeed = playerMetas.additiveProjectileSpeed;
        percentageProjectileSpeed = playerMetas.percentageProjectileSpeed;
        additiveDuration = playerMetas.additiveDuration;
        percentageDuration = playerMetas.percentageDuration;
        additiveExperience = playerMetas.additiveExperience;
        percentageExperience = playerMetas.percentageExperience;

        moveSpeed = (moveSpeed + additivePlayerMoveSpeed) * percentagePlayerMoveSpeed;
    }

    public enum Binding
    {
        Up,
        Down,
        Left,
        Right
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            listener.enabled = true;
            vc.Priority = 1;
            LoaclInstance = this;
            OnAnyPlayerSpawned?.Invoke(this, new OnAnyPlayerSpawnedEventArgs
            {
                player = Player.LoaclInstance.NetworkObject,
                clientId = Player.LoaclInstance.OwnerClientId,
            });
            if(!IsServer)
            {
                this.TryGetComponent<NetworkObject>(out var playerObject);
                SyncPlayerStatsRPC(
                    playerObject,
                    additiveMaxHealthModifier,
                    percentageMaxHealthModifier,
                    additiveDamageModifier,
                    percentageDamageModifier,
                    percentageCooldownModifier,
                    additiveProjectileModifier,
                    additiveAreaModifier,
                    percentageAreaModifier,
                    enemySpawnModifier,
                    enemyDamageModifier,
                    playerHealthRegen,
                    percentagePlayerHealthRegen,
                    percentageTreasureFind,
                    percentageTreasurGain,
                    additivePlayerMoveSpeed,
                    percentagePlayerMoveSpeed,
                    additiveProjectileSpeed,
                    percentageProjectileSpeed,
                    additiveDuration,
                    percentageDuration,
                    additiveExperience,
                    percentageExperience
                    );
            }

        }
        else
        {
            vc.Priority = 0;
        }

        if(IsServer) 
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId) 
        {
            //Debug.Log(PlayerHealth._allPlayers[clientId].ToString());
            PlayerHealth._allPlayers.Remove(clientId);
            //Debug.Log(PlayerHealth._allPlayers[clientId].ToString());
        }
    }

    void Update()
    {
        if(!IsOwner) return;
    }

    private void FixedUpdate()
    {
        move();
    }

    [Rpc(SendTo.Server)]
    private void SyncPlayerStatsRPC(
        NetworkObjectReference playerReference,
        int additiveMaxHealthModifier,
        float percentageMaxHealthModifier,
        int additiveDamageModifier,
        float percentageDamageModifier,
        float percentageCooldownModifier,
        int additiveProjectileModifier,
        int additiveAreaModifier,
        float percentageAreaModifier,
        float enemySpawnModifier,
        float enemyDamageModifier,
        int playerHealthRegen,
        float percentagePlayerHealthRegen,
        float percentageTreasureFind,
        float percentageTreasurGain,
        int additivePlayerMoveSpeed,
        float percentagePlayerMoveSpeed,
        int additiveProjectileSpeed,
        float percentageProjectileSpeed,
        int additiveDuration,
        float percentageDuration,
        int additiveExperience,
        float percentageExperience
        )
    {
        playerReference.TryGet(out NetworkObject player);
        player.TryGetComponent( out Player playerStats);
        playerStats.additiveMaxHealthModifier = additiveMaxHealthModifier;
        playerStats.percentageMaxHealthModifier = percentageMaxHealthModifier;
        playerStats.additiveDamageModifier = additiveDamageModifier;
        playerStats.percentageDamageModifier = percentageDamageModifier;
        playerStats.percentageCooldownModifier = percentageCooldownModifier;
        playerStats.additiveProjectileModifier = additiveProjectileModifier;
        playerStats.additiveAreaModifier = additiveAreaModifier;
        playerStats.percentageAreaModifier = percentageAreaModifier;
        playerStats.enemySpawnModifier = enemySpawnModifier;
        playerStats.enemyDamageModifier = enemyDamageModifier;
        playerStats.playerHealthRegen = playerHealthRegen;
        playerStats.percentageTreasureFind = percentageTreasureFind;
        playerStats.percentageTreasurGain = percentageTreasurGain;
        playerStats.additivePlayerMoveSpeed = additivePlayerMoveSpeed;
        playerStats.percentagePlayerMoveSpeed = percentagePlayerMoveSpeed;
        playerStats.additiveProjectileSpeed = additiveProjectileSpeed;
        playerStats.percentageProjectileSpeed = percentageProjectileSpeed;
        playerStats.additiveDuration = additiveDuration;
        playerStats.percentageDuration = percentageDuration;
        playerStats.additiveExperience = additiveExperience;
        playerStats.percentageExperience = percentageExperience;

    }

    private void move()
    {
        Vector2 playerVelocity = moveAction.ReadValue<Vector2>();

        rb.linearVelocity = new Vector2(playerVelocity.x * moveSpeed, playerVelocity.y * moveSpeed);
    }

    public ulong GetPlayerId()
    {
        return NetworkObjectId;
    }

    public override void OnDestroy()
    {
        if(!IsOwner) { return; }
        moveAction.Dispose();
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Up:
                return moveAction.GetBindingDisplayString(2);
            case Binding.Down:
                return moveAction.GetBindingDisplayString(4);
            case Binding.Left:
                return moveAction.GetBindingDisplayString(6);
            case Binding.Right:
                return moveAction.GetBindingDisplayString(8);

        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        InputSystem.actions.Disable();

        int bindingIndex;

        switch (binding)
        {
            default:
                case Binding.Up:
                bindingIndex = 2;
                break;
                case Binding.Down:
                bindingIndex = 4;
                break;
                case Binding.Left:
                bindingIndex = 6;
                break;
                case Binding.Right:
                bindingIndex = 8;
                break;
        }

        moveAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                //Debug.Log(callback.action.bindings[0].path);
                //Debug.Log(callback.action.bindings[0].overridePath);
                callback.Dispose();
                InputSystem.actions.Enable();
                onActionRebound();

            }).Start();
    }
}
