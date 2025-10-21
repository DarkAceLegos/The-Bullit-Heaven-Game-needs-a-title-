using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine.EventSystems;
using System;
using Unity.Services.Matchmaker.Models;

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

    [SerializeField] private Rigidbody2D rb;

    InputAction moveAction;

    [SerializeField] private CinemachineCamera vc;
    [SerializeField] private AudioListener listener;
    [SerializeField] PlayerVisual playerVisual;

    [SerializeField] private List<AttackData> allAttacksPlayerUnlocked = new();

    public List<AttackData> GetAllPlayerUnlockedAttacks() { return allAttacksPlayerUnlocked; }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));
        //playerMetas.SetPlayerMetaProgressionPercentDamageModifier(playerData.playerMetaProgression.percentageDamageModifier);
        //Debug.Log(moveAction.GetBindingDisplayString(1));
        //Debug.Log(GetBindingText(Binding.Up));
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
