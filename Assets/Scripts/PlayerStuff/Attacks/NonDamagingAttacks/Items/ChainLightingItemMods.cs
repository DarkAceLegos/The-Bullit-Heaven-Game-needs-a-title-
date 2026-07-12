using System.Collections.Generic;
using Unity.Netcode;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ChainLightingItemMods : Attack
{
    private ItemAttackData.LevelData levelData;
    public Item item;
    //public ChainLithningAttackModItems itemChoses;

    public override void Tick(NetworkObject player)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (ItemAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        var player = transform.root.GetComponent<Player>();

        //OnHitTester onHitTester = new OnHitTester();
        //player.items.Add(new ItemList(onHitTester, onHitTester.GiveName(), (int)levelData.value));

        //player.GetComponentInChildren<ChainLightiningAttack>()

        item = GetItem(levelData.index);
        //player.GetComponentInChildren<ChainLightiningAttack>().items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));

        var chainLightiningAttack = player.GetComponentInChildren<ChainLightiningAttack>();

        foreach (var i in chainLightiningAttack.items) 
        { 
            if(i.name == item.GiveName())
            { 
                i.stacks += levelData.numStacks; 
                return;
            }
        }

        chainLightiningAttack.items.Add(new ItemList(item, item.GiveName(), 1));
    }

    public Item GetItem(int item)
    {
        switch (item) 
        { 
            case (int)ChainLithningAttackModItems.ProjCount:
                return new BasicAttackModProjCount();
            case (int)ChainLithningAttackModItems.Damage:
                return new BasicAttackModDamage();
            case (int)ChainLithningAttackModItems.Cooldown:
                return new BasicAttackModCooldown();
            case (int)ChainLithningAttackModItems.Speed:
                return new BasicAttackModSpeed();
            default:
                return null;
        }
    
    
    }
}

public enum ChainLithningAttackModItems
{
    ProjCount,
    Damage,
    Cooldown,
    Speed
}
