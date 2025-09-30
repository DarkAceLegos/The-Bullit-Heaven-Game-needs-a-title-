using Unity.Netcode;
using UnityEngine;

public class BasicAttack : Attack
{
    [SerializeField] private BasicProj proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    private void Update()
    {
        if(!IsSpawned) { return; }

        //Tick(GameManager.Instance.playerList[0]);
    }

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;
        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        if(lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < levelData.projCount; i++)
        {
            var direction = Random.insideUnitCircle;
            direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position , Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(levelData.damage, levelData.speed);//*/
        }
    }
}
