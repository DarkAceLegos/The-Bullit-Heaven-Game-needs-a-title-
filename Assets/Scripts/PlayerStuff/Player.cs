using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IDataPersistence
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
    [SerializeField] public int additivePickUpRange;
    [SerializeField] public float percentagePickUpRange = 1f;
    [SerializeField] public int additiveAuraArea;
    [SerializeField] public float percentageAuraArea = 1f;
    [SerializeField] public int additiveAuraEffect;
    [SerializeField] public float percentageAuraEffect = 1f;

    [SerializeField] private Rigidbody2D rb;

    public Vector2 direction;

    //InputAction moveAction;

    [SerializeField] private CinemachineCamera vc;
    [SerializeField] private AudioListener listener;
    [SerializeField] PlayerVisual playerVisual;
    [SerializeField] private List<Vector2> spawnPositions;

    [SerializeField] public List<AttackData> allAttacksPlayerUnlocked = new();

    [SerializeField] private List<AttackData> noOtherAttacks = new();

    [SerializeField] private Collector collector;
    [SerializeField] private PickUpRange pickUpRange;

    public List<AttackData> GetAllPlayerUnlockedAttacks() { return allAttacksPlayerUnlocked; }
    public List<AttackData> GetPlayerNoOtherAttacks() { return noOtherAttacks; }

    void Start()
    {
        //moveAction = InputSystem.actions.FindAction("Move");//

        PlayerData playerData = GameMultiplayerConnectionAppoval.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayerConnectionAppoval.Instance.GetPlayerColor(playerData.colorId));

        foreach (var attackId in playerMetas.attackCardDeck)
        {
            GameManager.Instance.allAttacks.TryGetValue(attackId.attackId, out AttackData attack);
            allAttacksPlayerUnlocked.Add(attack);
        }

        foreach (var attackId in playerMetas.attackCardDeckLocks)
        {
            GameManager.Instance.allAttacks.TryGetValue(attackId.attackId, out AttackData attack);
            allAttacksPlayerUnlocked.Add(attack);
        }

        if (!IsServer)
        {
            this.TryGetComponent<NetworkObject>(out var playerObject);
            SyncPlayerStatsRPC(
                LoaclInstance.OwnerClientId,
                LoaclInstance.additiveMaxHealthModifier,
                LoaclInstance.percentageMaxHealthModifier,
                LoaclInstance.additiveDamageModifier,
                LoaclInstance.percentageDamageModifier,
                LoaclInstance.percentageCooldownModifier,
                LoaclInstance.additiveProjectileModifier,
                LoaclInstance.additiveAreaModifier,
                LoaclInstance.percentageAreaModifier,
                LoaclInstance.enemySpawnModifier,
                LoaclInstance.enemyDamageModifier,
                LoaclInstance.playerHealthRegen,
                LoaclInstance.percentagePlayerHealthRegen,
                LoaclInstance.percentageTreasureFind,
                LoaclInstance.percentageTreasurGain,
                LoaclInstance.additivePlayerMoveSpeed,
                LoaclInstance.percentagePlayerMoveSpeed,
                LoaclInstance.additiveProjectileSpeed,
                LoaclInstance.percentageProjectileSpeed,
                LoaclInstance.additiveDuration,
                LoaclInstance.percentageDuration,
                LoaclInstance.additiveExperience,
                LoaclInstance.percentageExperience, 
                LoaclInstance.additivePickUpRange, 
                LoaclInstance.percentagePickUpRange, 
                LoaclInstance.additiveAuraArea, 
                LoaclInstance.percentageAuraArea, 
                LoaclInstance.additiveAuraEffect, 
                LoaclInstance.percentageAuraEffect
                );
        }

        moveSpeed = (moveSpeed + additivePlayerMoveSpeed) * percentagePlayerMoveSpeed;

        direction = Vector2.right;
    }

    public enum Binding//
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
            collector.enabled = true;
            collector.GetComponent<Collider2D>().enabled = true;
            pickUpRange.enabled = true;
            pickUpRange.GetComponent<Collider2D>().enabled = true;
            transform.position = spawnPositions[(int)OwnerClientId];
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

        if (IsServer)
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
        if (!IsOwner) return;
    }

    private void FixedUpdate()
    {
        move();//
    }

    [Rpc(SendTo.Server)]
    private void SyncPlayerStatsRPC(
        ulong playerId,
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
        float percentageExperience,
        int additivePickUpRange,
        float percentagePickUpRange,
        int additiveAuraArea,
        float percentageAuraArea ,
        int additiveAuraEffect,
        float percentageAuraEffect
        )
    {
        Debug.Log("Trying to sync");

        PlayerHealth._allPlayers[playerId].transform.root.TryGetComponent<Player>(out var player);
        //Debug.Log("Change this " + player.additiveMaxHealthModifier + " by adding this " + additiveMaxHealthModifier);
        player.additiveMaxHealthModifier = additiveMaxHealthModifier;
        //Debug.Log("to this " + player.additiveMaxHealthModifier);
        player.percentageMaxHealthModifier = percentageMaxHealthModifier;
        player.additiveDamageModifier = additiveDamageModifier;
        player.percentageDamageModifier = percentageDamageModifier;
        player.percentageCooldownModifier = percentageCooldownModifier;
        player.additiveProjectileModifier = additiveProjectileModifier;
        player.additiveAreaModifier = additiveAreaModifier;
        player.percentageAreaModifier = percentageAreaModifier;
        player.enemySpawnModifier = enemySpawnModifier;
        player.enemyDamageModifier = enemyDamageModifier;
        player.playerHealthRegen = playerHealthRegen;
        player.percentageTreasureFind = percentageTreasureFind;
        player.percentageTreasurGain = percentageTreasurGain;
        player.additivePlayerMoveSpeed = additivePlayerMoveSpeed;
        player.percentagePlayerMoveSpeed = percentagePlayerMoveSpeed;
        player.additiveProjectileSpeed = additiveProjectileSpeed;
        player.percentageProjectileSpeed = percentageProjectileSpeed;
        player.additiveDuration = additiveDuration;
        player.percentageDuration = percentageDuration;
        player.additiveExperience = additiveExperience;
        player.percentageExperience = percentageExperience;
        player.additivePickUpRange = additivePickUpRange;
        player.percentagePickUpRange = percentagePickUpRange;
        player.additiveAuraArea = additiveAuraArea;
        player.percentageAuraArea = percentageAuraArea;
        player.additiveAuraEffect = additiveAuraEffect;
        player.percentageAuraEffect = percentageAuraEffect;
    }

    private void move()
    {
        Vector2 playerVelocity = GameInputs.Instance.GetMovmentVectorNormilzed(); //moveAction.ReadValue<Vector2>();// may need to cahnge how diection is found

        if (playerVelocity == Vector2.up)
        {direction = playerVelocity;}
        else if (playerVelocity == Vector2.down)
        {direction = playerVelocity;} 
        else if (playerVelocity == Vector2.left)
        { direction = playerVelocity; }
        else if (playerVelocity == Vector2.right)
        { direction = playerVelocity;}
    
        rb.linearVelocity = new Vector2(playerVelocity.x * moveSpeed, playerVelocity.y * moveSpeed);//
    }

    public ulong GetPlayerId()
    {
        return OwnerClientId;
    }

    public override void OnDestroy()
    {
        if(!IsOwner) { return; }
        //moveAction.Dispose();
    }

    public void LoadData(GameData progression)
    {
        Debug.Log("Loading Player data");
        this.additiveMaxHealthModifier = progression.additiveMaxHealthModifier;
        this.percentageMaxHealthModifier = progression.percentageMaxHealthModifier;
        this.additiveDamageModifier = progression.additiveDamageModifier;
        this.percentageDamageModifier = progression.percentageDamageModifier;
        this.percentageCooldownModifier = progression.percentageCooldownModifier;
        this.additiveProjectileModifier = progression.additiveProjectileModifier;
        this.additiveAreaModifier = progression.additiveAreaModifier;
        this.percentageAreaModifier = progression.percentageAreaModifier;
        this.enemySpawnModifier = progression.enemySpawnModifier;
        this.enemyDamageModifier = progression.enemyDamageModifier;
        this.playerHealthRegen = progression.playerHealthRegen;
        this.percentagePlayerHealthRegen = progression.percentagePlayerHealthRegen;
        this.percentageTreasureFind = progression.percentageTreasureFind;
        this.percentageTreasurGain = progression.percentageTreasurGain;
        this.additivePlayerMoveSpeed = progression.additivePlayerMoveSpeed;
        this.percentagePlayerMoveSpeed = progression.percentagePlayerMoveSpeed;
        this.additiveProjectileSpeed = progression.additiveProjectileSpeed;
        this.percentageProjectileSpeed = progression.percentageProjectileSpeed;
        this.additiveDuration = progression.additiveDuration;
        this.percentageDuration = progression.percentageDuration;
        this.additiveExperience = progression.additiveExperience;
        this.percentageExperience = progression.percentageExperience;
        this.additivePickUpRange = progression.additivePickUpRange;
        this.percentagePickUpRange = progression.percentagePickUpRange;
        this.additiveAuraArea = progression.additiveAuraArea;
        this.percentageAuraArea = progression.percentageAuraArea;
        this.additiveAuraEffect = progression.additiveAuraEffect;
        this.percentageAuraEffect = progression.percentageAuraEffect;
    }

    public void SaveData(ref GameData progression)
    {
        //throw new NotImplementedException();
    }
}
