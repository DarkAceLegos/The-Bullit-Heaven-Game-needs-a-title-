using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AttackHandler : NetworkBehaviour
{
    private Dictionary<string, Attack> activeAttacks = new();
    [SerializeField] private List<AttackData> initialAttacks = new();

    public static AttackHandler LoaclInstance { get; private set; }

    // Need to keep watch of this to see if it is corect
    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;

        if (!IsOwner && !IsServer) return;

        LoaclInstance = this;

        Debug.Log("I am the owner");

        foreach (var attack in initialAttacks)
        {
            addAttack(attack);
            Debug.Log("we got an attack " +  attack.attackId);
        }
    }

    public void addAttack(AttackData data)
    {
        if(activeAttacks.TryGetValue(data.attackId, out var attack1))
        {
            int newLevel = attack1.level + 1;
            attack1.initialize(data, newLevel);
            return;
        }
        var attack = Instantiate(data.prefab, transform);
        attack.initialize(data, 0);
        activeAttacks[data.attackId] = attack;

        Debug.Log("added the attack " + data.prefab + " " + transform);
    }

    public int getLevel(string id)
    {
        return activeAttacks.TryGetValue(id,out var attack1) ? attack1.level : 0;
    }

    private void Update()
    {
        if(!IsOwner) return;

        if(!enabled) return;

        if(!IsSpawned) return;

        //Debug.Log("tryed to tick out of Rpc with " + GameManager.Instance.playerList[0]);

        foreach (var attack in activeAttacks)
        {
            //Debug.Log("tryed to tick");

            SpawnProjectileRpc(GameManager.Instance.playerList[0], attack.Key);

            //Debug.Log("this is the key " + attack.Key);
        }

        //Debug.Log("After the rpc");

    }

    [Rpc(SendTo.Server)]
    private void SpawnProjectileRpc(NetworkObjectReference playerReference, string key)
    {
        //Debug.Log("trying the Rpc");

        playerReference.TryGet(out NetworkObject player);

        //Debug.Log("the player " + player);

        activeAttacks.TryGetValue(key, out Attack attack);

        //Debug.Log("this is the attack " + attack);

        attack.Tick(player);

        //Debug.Log("trying the Rpc post" + activeAttacks);

    }
}
