using Unity.Netcode;
using UnityEngine;

public class AuraAttackMods : Attack
{
    private ItemAttackData.LevelData levelData;
    public Item item;
    //public ChainLithningAttackModItems itemChoses;

    public override void Tick(NetworkObject player, int Direction = 0, bool skipCooldown = false)
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

        //item = GetItem(levelData.index[0]);
        //player.GetComponentInChildren<ChainLightiningAttack>().items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));

        //var chainLightiningAttack = player.GetComponentInChildren<AuraAttack>();

        for (int j = 0; j < levelData.index.Count; j++)
        {
            GiveItems(player, j);

            /*item = GetItem(levelData.index[j]);

            foreach (var i in chainLightiningAttack.items)
            {
                if (i.name == item.GiveName())
                {
                    i.stacks += levelData.numStacks[j];
                    return;
                }
            }

            chainLightiningAttack.items.Add(new ItemList(item, item.GiveName(), levelData.numStacks[j]));*/
        }
    }

    public Item GetItem(int item)
    {
        switch (item)
        {
            case (int)AOEAttackModsItems.ProjCount:
                return new AOEAttackModProjCount();
            case (int)AOEAttackModsItems.Damage:
                return new AOEAttackModDamage();
            case (int)AOEAttackModsItems.Cooldown:
                return new AOEAttackModCooldown();
            case (int)AOEAttackModsItems.Speed:
                return new AOEAttackModSpeed();
            case (int)AOEAttackModsItems.Area:
                return new AOEAttackModArea();
            default:
                return null;
        }
    }

    private void GiveItems(Player player, int j)
    {
        var chainLightiningAttack = player.GetComponentInChildren<AuraAttack>();

        item = GetItem(levelData.index[j]);

        foreach (var i in chainLightiningAttack.items)
        {
            if (i.name == item.GiveName())
            {
                i.stacks += levelData.numStacks[j];
                return;
            }
        }

        chainLightiningAttack.items.Add(new ItemList(item, item.GiveName(), levelData.numStacks[j]));
    }
}


public enum AOEAttackModsItems
{
    ProjCount,
    Damage,
    Cooldown,
    Speed,
    Area
}
