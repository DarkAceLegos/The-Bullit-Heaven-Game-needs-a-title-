using Unity.Netcode;
using UnityEngine;

public class ShootCloseAttackMods : Attack
{
    private ItemAttackData.LevelData levelData;
    public Item item;
    //public ChainLithningAttackModItems itemChoses;

    public override void Tick(NetworkObject player, int Direction = 0)
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

        var chainLightiningAttack = player.GetComponentInChildren<ShootCloseestEnemyAttack>();

        foreach (var i in chainLightiningAttack.items)
        {
            if (i.name == item.GiveName())
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
            case (int)BasicAttackModItems.ProjCount:
                return new BasicAttackModProjCount();
            case (int)BasicAttackModItems.Damage:
                return new BasicAttackModDamage();
            case (int)BasicAttackModItems.Cooldown:
                return new BasicAttackModCooldown();
            case (int)BasicAttackModItems.Speed:
                return new BasicAttackModSpeed();
            default:
                return null;
        }
    }
}
