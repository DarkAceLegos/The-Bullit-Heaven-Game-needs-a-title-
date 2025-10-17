using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class AttackHandler : NetworkBehaviour
{
    private Dictionary<string, Attack> activeAttacks = new();
    [SerializeField] private List<AttackData> initialAttacks = new();

    //[SerializeField] private Array veiwingActiveAttacks;

    public static AttackHandler LoaclInstance { get; private set; }

    private AttackData newAttack;

    private List<AttackData> attackList /*= Player.LoaclInstance.GetAllPlayerUnlockedAttacks()*/;

    // Need to keep watch of this to see if it is corect
    public override void OnNetworkSpawn()
    {
        attackList = Player.LoaclInstance.GetAllPlayerUnlockedAttacks(); //caution will need to change to fix if some players have different unlocked attacks

        enabled = IsOwner;

        if (IsOwner) { LoaclInstance = this; }

        if (!IsOwner && !IsServer) return;

        //Debug.Log("I am the owner");

        foreach (var attack in initialAttacks)
        {
            addAttack(attack);
            //Debug.Log("we got an attack " +  attack.attackId);
        }
    }

    public void addAttack(AttackData data)
    {
        if(activeAttacks.TryGetValue(data.attackId, out var attack1))
        {
            //Debug.Log($"attack {data.name} leveled up now it has {attack1.level}");

            int newLevel = attack1.level + 1;
            
            //Debug.Log($"attack {data.name} leveled up now it has {newLevel}");

            attack1.initialize(data, newLevel);

            if (!IsServer) { SyncPlayersActiveAttacksWhenLevelingUpAnAttackRpc(Player.LoaclInstance.OwnerClientId, data.attackId, newLevel); }

            //Debug.Log($"attack {data.name} leveled up now it has {attack1.data}");

            return;
        }
        var attack = Instantiate(data.prefab, transform);
        attack.initialize(data, 0);
        activeAttacks[data.attackId] = attack;

        if (!IsServer) 
        {
            SyncPlayersActiveAttacksWhenGettingNewAttackRpc(Player.LoaclInstance.OwnerClientId, data.attackId);
        }

        //Debug.Log($" we got {data.name} attack");
        //Debug.Log("added the attack " + data.prefab + " " + transform);
    }

    [Rpc(SendTo.Server)]
    private void SyncPlayersActiveAttacksWhenLevelingUpAnAttackRpc(ulong playerId, string attackId, int Level/*take in data and level for the client players*/)
    {
        //initialize the attack data and level on server side so it can spawn proporly

        PlayerHealth._allPlayers[playerId].TryGetComponent<AttackHandler>(out var attackHandlerServerClient);

        attackHandlerServerClient.activeAttacks.TryGetValue(attackId, out var attack);

        //Debug.Log(attack);

        attack.initialize(attack.data, Level);
    }

    [Rpc(SendTo.Server)]
    private void SyncPlayersActiveAttacksWhenGettingNewAttackRpc(ulong playerId, string attackId)
    {
        PlayerHealth._allPlayers[playerId].TryGetComponent<AttackHandler>(out var attackHandlerServerClient);

        for (int i = 0; i < attackList.Count; i++)
        { 
            if(attackId == attackList[i].attackId) 
            { 
                //Debug.Log(attackList[i]);

                newAttack = attackList[i];

                var attack = Instantiate(newAttack.prefab, attackHandlerServerClient.transform);

                attack.initialize(newAttack, 0);

                attackHandlerServerClient.activeAttacks[attackList[i].attackId] = attack;
            }
        }
    }

    public int getLevel(string id)
    {
        return activeAttacks.TryGetValue(id,out var attack1) ? attack1.level : -1;
    }

    private void Update()
    {
        if(!IsOwner) return;

        if(!enabled) return;

        if(!IsSpawned) return;

        //Debug.Log("tryed to tick out of Rpc with " + GameManager.Instance.playerList[0]);

        PlayerHealth._allPlayers.TryGetValue(Player.LoaclInstance.OwnerClientId, out PlayerHealth player); // need to make it not get every update

        //Debug.Log(player);

        player.TryGetComponent<NetworkObject>(out var playerObject); // need to make it not get every update

        foreach (var attack in activeAttacks)
        {
            //Debug.Log("tryed to tick");

            SpawnProjectileRpc( 
                playerObject
                //GameManager.Instance.playerList[0]
                , attack.Key); //needs to be the exact player not just the first player in list 

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

        //Debug.Log(key);

        player.GetComponent<AttackHandler>().activeAttacks.TryGetValue(key, out Attack attack);

        //Debug.Log("this is the attack " + attack);

        attack.Tick(player);

        //Debug.Log("trying the Rpc post" + activeAttacks);

    }
}
