using UnityEngine;


[System.Serializable]
public abstract class Item 
{
    public abstract string GiveName();
    public virtual void Update(Player player, int stacks) { }

    public virtual void OnHit(Player player , EnemyHealth enemyHealth, int stacks) { }

    public virtual void OnDestroyed(Player player, int stacks) { }

    public virtual BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData) { return basicAttackData; }
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

public class BasicAttackModProjCount : Item
{
    public override string GiveName()
    {
        return "BasicAttackModProjCount";
    }

    public override BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount += stacks;

        return basicAttackData;
    }
}

public class BasicAttackModDamage : Item
{
    public override string GiveName()
    {
        return "BasicAttackModDamage";
    }

    public override BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage += stacks;

        return basicAttackData;
    }
}

public class BasicAttackModCooldown : Item
{
    public override string GiveName()
    {
        return "BasicAttackModCooldown";
    }

    public override BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown -= stacks;

        return basicAttackData;
    }
}

public class BasicAttackModSpeed : Item
{
    public override string GiveName()
    {
        return "BasicAttackModSpeed";
    }

    public override BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed += stacks;

        return basicAttackData;
    }
}
public class AOEAttackModProjCount : Item
{
    public override string GiveName()
    {
        return "AOEAttackModProjCount";
    }

    public override AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount += stacks;

        return basicAttackData;
    }
}

public class AOEAttackModDamage : Item
{
    public override string GiveName()
    {
        return "AOEAttackModDamage";
    }

    public override AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage += stacks;

        return basicAttackData;
    }
}

public class AOEAttackModCooldown : Item
{
    public override string GiveName()
    {
        return "AOEAttackModCooldown";
    }

    public override AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown -= stacks;

        return basicAttackData;
    }
}

public class AOEAttackModSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed += stacks;

        return basicAttackData;
    }
}
public class AOEAttackModArea : Item
{
    public override string GiveName()
    {
        return "AOEAttackModArea";
    }

    public override AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.area += stacks;

        return basicAttackData;
    }
}
public class FollowingAttackModProjCount : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModProjCount";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount += stacks;

        return basicAttackData;
    }
}

public class FollowingAttackModDamage : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModDamage";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage += stacks;

        return basicAttackData;
    }
}

public class FollowingAttackModCooldown : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModCooldown";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown -= stacks;

        return basicAttackData;
    }
}

public class FollowingAttackModSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed += stacks;

        return basicAttackData;
    }
}
public class FolloingAttackModArea : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModArea";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.Area += stacks;

        return basicAttackData;
    }
}

public class AltProjMovement : Item
{
    public override string GiveName()
    {
        return "AltProjMovement";
    }


}