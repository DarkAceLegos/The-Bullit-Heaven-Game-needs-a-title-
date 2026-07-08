using UnityEngine;


[System.Serializable]
public abstract class Item 
{
    public abstract string GiveName();
    public virtual void Update(Player player, int stacks) { }

    public virtual void OnHit(Player player , EnemyHealth enemyHealth, int stacks) { }

    public virtual void OnDestroyed(Player player, int stacks) { }

    public virtual BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData) { return basicAttackData; }
}


public class HealingItem : Item
{
    public override string GiveName()
    {
        return "HealingItem";
    }

    public override void Update(Player player, int stacks)
    {
        player.GetComponentInChildren<PlayerHealth>().changeHealth(5);
    }
}

public class OnHitTester : Item
{
    public override string GiveName()
    {
        return "OnHitTest";
    }

    public override void OnHit(Player player, EnemyHealth enemyHealth, int stacks)
    {
        Debug.Log(enemyHealth.ToString() + " Hit enemy " + stacks);
    }
}

public class ModDataTester : Item
{
    public override string GiveName()
    {
        return "ModDataTester";
    }

    public override BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = 10;

        return basicAttackData;
    }
}