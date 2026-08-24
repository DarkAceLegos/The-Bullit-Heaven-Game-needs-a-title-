using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


[System.Serializable]
public abstract class Item 
{
    public abstract string GiveName();
    public virtual void Update(Player player, int stacks) { }
    public virtual void UpdateAttack(Player player, int stacks) { }
    public virtual void UpdateEnemy(EnemyHealth enemyHealth, int stacks) { }

    public virtual void OnHit(Player player , EnemyHealth enemyHealth, GameObject proj, int stacks) { }
    public virtual void OnHitPlayer(Player player, int stacks) { }

    public virtual void OnDestroyed(Player player, int stacks) { }
    public virtual void OnPlayerDie(Player player, int stacks) { }

    public virtual void OnEnemyDie(Player player, EnemyHealth enemyHealth, int stacks) { }
    public virtual void OnCast(Player player, int stacks) { }
    public virtual void OnLevelUp(Player player, int level, int stacks) { }

    public virtual GameObject AttackChangeProj(Player player, int stacks, GameObject proj) { return proj; }
    public virtual Vector3 AttackDirectionMod(Player player, int stacks, Vector3 destinaton, int projCount, int whichProj) { return destinaton; }
    public virtual Vector3 AttackStartLocationMod(Player player, int stacks, Vector3 startLocal, int projCount, int whichProj) { return startLocal; }

    public virtual BasicAttackData.LevelData BasicAttackDataMod(Player player, int stacks, BasicAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual AOEAttackData.LevelData AOEAttackDataMod(Player player, int stacks, AOEAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual FollowingAttackData.LevelData FollowingAttackDataMod(Player player, int stacks, FollowingAttackData.LevelData basicAttackData) { return basicAttackData; }

    public virtual BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData) { return basicAttackData; }
    public virtual FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData) { return basicAttackData; }
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

    public override void OnHit(Player player, EnemyHealth enemyHealth, GameObject proj, int stacks)
    {
        //Debug.Log(enemyHealth.ToString() + " Hit enemy " + stacks);

        //Ticks attack

        /*player.TryGetComponent(out NetworkObject networkObject);

        player.GetComponentInChildren<ChainLightiningAttack>().Tick(networkObject, 5);*/

        //Spwan a prefab

        /*GameObject proj = GameManager.Instance.GetPrefab(0);

        player.TryGetComponent(out NetworkObject networkObject);

        List<ItemList> items = new List<ItemList>();

        NetworkObject enemyNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(proj, player.transform.position, Quaternion.identity);

        enemyNetworkObject.GetComponent<ChainLightingProj>().Initialize(networkObject.OwnerClientId, 1, 1, items);
        enemyNetworkObject.GetComponent<ChainLightingProj>().prefab = proj;

        enemyNetworkObject.Spawn(true);*/
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
public class FollowingAttackModArea : Item
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

public class BasicAttackModProjCountScalingHp : Item
{
    public override string GiveName()
    {
        return "BasicAttackModProjCountScalingHp";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)((basicAttackData.projCount * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier) / 100);

        return basicAttackData;
    }
}
public class BasicAttackModDamageScalingHp : Item
{
    public override string GiveName()
    {
        return "BasicAttackModDamageScalingHp";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)((basicAttackData.damage * stacks * (player.additiveMaxHealthModifier +100) * player.percentageMaxHealthModifier) / 100);

        return basicAttackData;
    }
}
public class BasicAttackModCooldownScalingHp : Item
{
    public override string GiveName()
    {
        return "BasicAttackModCooldownScalingHp";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = (basicAttackData.cooldown * (-stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier) / 500);

        return basicAttackData;
    }
}
public class BasicAttackModSpeedScalingHp : Item
{
    public override string GiveName()
    {
        return "BasicAttackModSpeedScalingHp";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (player.percentageMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 500;

        return basicAttackData;
    }
}
public class AOEAttackModProjCountScalingHp : Item
{
    public override string GiveName()
    {
        return "AOEAttackModProjCountScalingHp";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100);

        return basicAttackData;
    }
}
public class AOEAttackModDamageScalingHp : Item
{
    public override string GiveName()
    {
        return "AOEAttackModDamageScalingHp";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100);

        return basicAttackData;
    }
}
public class AOEAttackModCooldownScalingHp : Item
{
    public override string GiveName()
    {
        return "AOEAttackModCooldownScalingHp";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 500;

        return basicAttackData;
    }
}
public class AOEAttackModSpeedScalingHp : Item
{
    public override string GiveName()
    {
        return "AOEAttackModSpeedScalingHp";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100;

        return basicAttackData;
    }
}
public class AOEAttackModAreaScalingHp : Item
{
    public override string GiveName()
    {
        return "AOEAttackModAreaScalingHp";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.area = basicAttackData.area * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100;

        return basicAttackData;
    }
}
public class FollowingAttackModProjCountScalingHp : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModProjCountScalingHp";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 200);

        return basicAttackData;
    }
}
public class FollowingAttackModDamageScalingHp : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModDamageScalingHp";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100);

        return basicAttackData;
    }
}
public class FollowingAttackModCooldownScalingHp : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModCooldownScalingHp";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 500;

        return basicAttackData;
    }
}
public class FollowingAttackModSpeedScalingHp : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModSpeedScalingHp";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100;

        return basicAttackData;
    }
}
public class FollowingAttackModAreaScalingHp : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModAreaScalingHp";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.Area = basicAttackData.Area * stacks * (player.additiveMaxHealthModifier + 100) * player.percentageMaxHealthModifier / 100;

        return basicAttackData;
    }
}

public class BasicAttackModProjCountScalingSpeed : Item
{
    public override string GiveName()
    {
        return "BasicAttackModProjCountScalingSpeed";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class BasicAttackModDamageScalingSpeed : Item
{
    public override string GiveName()
    {
        return "BasicAttackModDamageScalingSpeed";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class BasicAttackModCooldownScalingSpeed : Item
{
    public override string GiveName()
    {
        return "BasicAttackModCooldownScalingSpeed";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class BasicAttackModSpeedScalingSpeed : Item
{
    public override string GiveName()
    {
        return "BasicAttackModSpeedScalingSpeed";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class AOEAttackModProjCountScalingSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModProjCountScalingSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class AOEAttackModDamageScalingSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModDamageScalingSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class AOEAttackModCooldownScalingSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModCooldownScalingSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class AOEAttackModSpeedScalingSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModSpeedScalingSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class AOEAttackModAreaScalingSpeed : Item
{
    public override string GiveName()
    {
        return "AOEAttackModAreaScalingSpeed";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.area = basicAttackData.area * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class FollowingAttackModProjCountScalingSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModProjCountScalingSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class FollowingAttackModDamageScalingSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModDamageScalingSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class FollowingAttackModCooldownScalingSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModCooldownScalingSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed;

        return basicAttackData;
    }
}
public class FollowingAttackModSpeedScalingSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModSpeedScalingSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = (basicAttackData.speed * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}
public class FollowingAttackModAreaScalingSpeed : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModAreaScalingSpeed";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.Area = (basicAttackData.Area * stacks * (player.additivePlayerMoveSpeed + 4) * player.percentagePlayerMoveSpeed);

        return basicAttackData;
    }
}

public class BasicAttackModProjCountScalingLevel : Item
{
    public override string GiveName()
    {
        return "BasicAttackModProjCountScalingLevel";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class BasicAttackModDamageScalingLevel : Item
{
    public override string GiveName()
    {
        return "BasicAttackModDamageScalingLevel";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class BasicAttackModCooldownScalingLevel : Item
{
    public override string GiveName()
    {
        return "BasicAttackModCooldownScalingLevel";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (LevelManager.Instance.level + 1);

        return basicAttackData;
    }
}
public class BasicAttackModSpeedScalingLevel : Item
{
    public override string GiveName()
    {
        return "BasicAttackModSpeedScalingLevel";
    }

    public override BasicAttackData.LevelData BasicAttackDataModScaling(Player player, int stacks, BasicAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = basicAttackData.speed * stacks * (LevelManager.Instance.level + 1);

        return basicAttackData;
    }
}
public class AOEAttackModProjCountScalingLevel : Item
{
    public override string GiveName()
    {
        return "AOEAttackModProjCountScalingLevel";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class AOEAttackModDamageScalingLevel : Item
{
    public override string GiveName()
    {
        return "AOEAttackModDamageScalingLevel";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class AOEAttackModCooldownScalingLevel : Item
{
    public override string GiveName()
    {
        return "AOEAttackModCooldownScalingLevel";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (LevelManager.Instance.level + 1);

        return basicAttackData;
    }
}
public class AOEAttackModSpeedScalingLevel : Item
{
    public override string GiveName()
    {
        return "AOEAttackModSpeedScalingLevel";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = (basicAttackData.speed * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class AOEAttackModAreaScalingLevel : Item
{
    public override string GiveName()
    {
        return "AOEAttackModAreaScalingLevel";
    }

    public override AOEAttackData.LevelData AOEAttackDataModScaling(Player player, int stacks, AOEAttackData.LevelData basicAttackData)
    {
        basicAttackData.area = basicAttackData.area * stacks * (LevelManager.Instance.level + 1);

        return basicAttackData;
    }
}
public class FollowingAttackModProjCountScalingLevel : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModProjCountScalingLevel";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.projCount = (int)(basicAttackData.projCount * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class FollowingAttackModDamageScalingLevel : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModDamageScalingLevel";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.damage = (int)(basicAttackData.damage * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class FollowingAttackModCooldownScalingLevel : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModCooldownScalingLevel";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.cooldown = basicAttackData.cooldown * -stacks * (LevelManager.Instance.level + 1);

        return basicAttackData;
    }
}
public class FollowingAttackModSpeedScalingLevel : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModSpeedScalingLevel";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.speed = (basicAttackData.speed * stacks * (LevelManager.Instance.level + 1));

        return basicAttackData;
    }
}
public class FollowingAttackModAreaScalingLevel : Item
{
    public override string GiveName()
    {
        return "FollowingAttackModAreaScalingLevel";
    }

    public override FollowingAttackData.LevelData FollowingAttackDataModScaling(Player player, int stacks, FollowingAttackData.LevelData basicAttackData)
    {
        basicAttackData.Area = basicAttackData.Area * stacks * (LevelManager.Instance.level + 1);

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