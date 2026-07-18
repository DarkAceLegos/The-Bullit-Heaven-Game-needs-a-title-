using Unity.Netcode;
using UnityEngine;

public class TestingAddingItems : Attack
{
    private NonDamagingAttackData.LevelData levelData;
    public override void Tick(NetworkObject player, int Direction = 0)
    {
        return;
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (NonDamagingAttackData)data;
        levelData = basicAttackData.GetLevelData(level);

        var player = transform.root.GetComponent<Player>();

        //OnHitTester onHitTester = new OnHitTester();
        //player.items.Add(new ItemList(onHitTester, onHitTester.GiveName(), (int)levelData.value));

        //player.GetComponentInChildren<ChainLightiningAttack>()

        ModDataTester onHitTester = new ModDataTester();
        player.GetComponentInChildren<ChainLightiningAttack>().items.Add(new ItemList(onHitTester, onHitTester.GiveName(), 1));
    }
}
