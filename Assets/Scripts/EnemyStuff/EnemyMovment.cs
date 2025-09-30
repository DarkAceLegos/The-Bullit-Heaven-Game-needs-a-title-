using System;
using Unity.Netcode;
using UnityEngine;

public class EnemyMovment : NetworkBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private PlayerHealth targetPlayer;
    private float lastPlayerCheck;

    public float moveSpeed;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void Update()
    {
        CheckForTargetPlayer();
    }

    private void CheckForTargetPlayer()
    {
        if (!IsServer) return;
        //Debug.Log("I am the server");


        if (lastPlayerCheck + 5 > Time.time) return;
        lastPlayerCheck = Time.time;

        //Debug.Log("time to check for a player");

        var allPlayer = PlayerHealth._allPlayers;
        if (allPlayer.Count <= 0) return;

        //Debug.Log("we have a list");
        
        ulong closestPlayer = default;
        float closestDistant = float.MaxValue;
        foreach (var player in allPlayer)
        {
            //Debug.Log("checking this " + player + player.Value.transform.position);

            var playerPos = player.Value.transform.position;
            var distance = Vector2.Distance(playerPos, rb.position);
            if (distance < closestDistant && !player.Value.isDowned) 
            { 
                closestPlayer = player.Key;
                //Debug.Log("the closest player is this " + closestPlayer);
                closestDistant = distance;
                //Debug.Log("the closest distant is this " + closestDistant);
            }
        }

        if (closestPlayer == default) return;

        //Debug.Log("we have closet player");

        //closestPlayer = 4;

        PlayerHealth._allPlayers.TryGetValue(closestPlayer, out targetPlayer);
    }

    private void FixedUpdate()
    {
        moveRpc();
    }

    [Rpc(SendTo.Server)]
    private void moveRpc()
    {
        //Debug.Log("trying to move 1st");

        if (!targetPlayer) return;

        var targetPos = targetPlayer.transform.position;
        var direction = (targetPos - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        //Debug.Log("trying to move");
    }

}
