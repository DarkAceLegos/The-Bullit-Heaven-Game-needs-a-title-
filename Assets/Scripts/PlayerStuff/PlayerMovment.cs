using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovment : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CinemachineCamera vc;
    [SerializeField] private AudioListener listener;
    [SerializeField] PlayerVisual playerVisual;
    [SerializeField] private List<Vector2> spawnPositions;

    public float moveSpeed;

    private void Start()
    {
        PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            listener.enabled = true;
            transform.position = spawnPositions[(int)OwnerClientId];
            vc.Priority = 1;
        }
        else
        {
            vc.Priority = 0;
        }
    }

    private void FixedUpdate()
    {
        move();//
    }

    private void move()
    {
        Vector2 playerVelocity = GameInputs.Instance.GetMovmentVectorNormilzed(); //moveAction.ReadValue<Vector2>();//

        rb.linearVelocity = new Vector2(playerVelocity.x * moveSpeed, playerVelocity.y * moveSpeed);//
    }
}
