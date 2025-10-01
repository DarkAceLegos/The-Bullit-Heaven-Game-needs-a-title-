using System;
using Unity.Netcode;
using UnityEngine;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int expToLevel = 5;

    [SerializeField] private int visualExpToNextLevel;

    [SerializeField] private int experiance = 0;
    [SerializeField] private int level = 0;

    public event EventHandler<OnExpChangeEventArgs> OnExpChange;
    public class OnExpChangeEventArgs : EventArgs
    {
        public int newExp;
    }

    public event EventHandler<OnLevelChangeEventArgs> OnLevelChange;
    public class OnLevelChangeEventArgs : EventArgs
    {
        public int newLevel;
    }

    [SerializeField] private int expToNextLevel => expToLevel * (level + 1);

    private void Awake()
    {
        Instance = this;
    }

    [Rpc(SendTo.Server)]
    public void AddExpRpc(int amount)
    {
        experiance += amount;
        SyncExpRpc(experiance);
        CheckForLeveling();

        //Debug.Log($"amount of exp is {experiance} and our level is {level}");
    }

    private void CheckForLeveling()
    {
        if (experiance < expToNextLevel) { return; }

        experiance -= expToNextLevel;
        level++;

        SyncExpRpc(experiance);
        SyncLevelRpc(level);
    }

    [Rpc(SendTo.Everyone)]
    private void SyncExpRpc(int newExp)
    { 
        experiance = newExp;
        OnExpChange?.Invoke(this, new OnExpChangeEventArgs
        {
            newExp = newExp,
        });
    }

    [Rpc(SendTo.Everyone)]
    private void SyncLevelRpc(int newLevel)
    {
        level = newLevel;
        visualExpToNextLevel = expToNextLevel;
        OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs
        {
            newLevel = newLevel,
        });
        Debug.Log("We leveled up");
    }
}
