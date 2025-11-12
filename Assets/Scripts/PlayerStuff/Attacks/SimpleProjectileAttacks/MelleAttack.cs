using Unity.Netcode;
using UnityEngine;

public class MelleAttack : Attack
{
    [SerializeField] private MelleProj proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    protected override void OnInitialize()
    {
        var basicAttackData = (BasicAttackData)data;

        //Debug.Log(basicAttackData);

        levelData = basicAttackData.GetLevelData(level);
    }

    public override void Tick(NetworkObject player)
    {
        //Debug.Log("in the tick");

        ulong playerId = player.OwnerClientId;

        player.TryGetComponent<Player>(out Player player1);

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        Vector3 Offset = player1.direction;

        for (int i = 0; i < levelData.projCount; i++)
        { 
            var rotation = Random.rotation;
            //if(player1.direction == Vector2(transform.up) || player1.direction == transform.down)

            //direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position + Offset, Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
