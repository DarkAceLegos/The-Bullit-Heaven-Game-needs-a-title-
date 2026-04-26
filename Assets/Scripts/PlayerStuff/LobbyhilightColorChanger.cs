using System;
using Unity.Netcode;
using UnityEngine;

public class LobbyhilightColorChanger : NetworkBehaviour
{
    [SerializeField] PlayerVisual playerVisual;

    private void Start()
    {
        GameMultiplayerConnectionAppoval.Instance.OnPlayerDataNetworkChanged += GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged;
        UpdateViausl();
    }

    private void GameMultiplayerConnectionAppoval_OnPlayerDataNetworkChanged(object sender, EventArgs e)
    {
        UpdateViausl();
    }

    private void UpdateViausl()
    {
        if (GameMultiplayerConnectionAppoval.Instance.IsPlayerIndexConnected(Convert.ToInt32(OwnerClientId)))
        {
            PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromClientId(OwnerClientId);
            playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));
        }
    }
}
