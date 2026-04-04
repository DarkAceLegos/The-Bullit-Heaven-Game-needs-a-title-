using System;
using Unity.Netcode;
using UnityEngine;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int expToLevel = 5;

    [SerializeField] private int visualExpToNextLevel;

    [SerializeField] public float experiance { get; private set; }
    [SerializeField] public int level = 0;

    public event EventHandler<OnExpChangeEventArgs> OnExpChange;
    public class OnExpChangeEventArgs : EventArgs
    {
        public float newExp;
    }

    public event EventHandler<OnLevelChangeEventArgs> OnLevelChange;
    public class OnLevelChangeEventArgs : EventArgs
    {
        public int newLevel;
    }

    [SerializeField] public int expToNextLevel { get; private set; }

    private void Awake()
    {
        Instance = this;
        expToNextLevel = NextExpLevel();
    }

    [Rpc(SendTo.Server)]
    public void AddExpRpc(float amount)
    {
        experiance += amount;
        SyncExpRpc(experiance);
        CheckForLeveling();

        //Debug.Log($"amount of exp is {experiance} and our level is {level}");
    }

    private void LateUpdate()
    {
        //if (experiance >= expToNextLevel) { AddExpRpc(0); Debug.Log("Adding 0 exp"); }
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
    private void SyncExpRpc(float newExp)
    { 
        experiance = newExp;
        LevelExpUi.instance.ExpChange(newExp);
        OnExpChange?.Invoke(this, new OnExpChangeEventArgs
        {
            newExp = newExp,
        });
    }

    [Rpc(SendTo.Everyone)]
    private void SyncLevelRpc(int newLevel)
    {
        level = newLevel;
        visualExpToNextLevel = NextExpLevel();
        OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs
        {
            newLevel = newLevel,
        });
        Debug.Log("We leveled up");
    }

    private int NextExpLevel()
    {
        if (level + 1 < 20) { expToNextLevel = (((level + 1) * 10) - 5); }
        else if(level + 1 == 20) { expToNextLevel = (((level + 1) * 10) - 5 + 600); }
        else if (level + 1 > 20 && level + 1 < 40) { expToNextLevel = (((level + 1) * 13) - 6); }
        else if (level + 1 == 40) { expToNextLevel = (((level + 1) * 13) - 6 + 2400); }
        else if (level + 1 > 40) { expToNextLevel = (((level + 1) * 16) - 8); }

        return expToNextLevel;
    }
}
