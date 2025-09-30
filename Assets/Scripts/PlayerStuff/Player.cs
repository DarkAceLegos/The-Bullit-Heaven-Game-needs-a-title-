using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine.EventSystems;
using System;

public class Player : NetworkBehaviour
{

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

    private void Awake()
    {
        
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
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
                clientId = NetworkObjectId,
            });
        }
        else
        {
            vc.Priority = 0;
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
}
