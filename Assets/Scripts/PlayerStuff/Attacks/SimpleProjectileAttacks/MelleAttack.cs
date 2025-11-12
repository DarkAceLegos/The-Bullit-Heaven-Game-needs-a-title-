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
            var rotation = Quaternion.identity;
            rotation.x = 0;
            rotation.y = 0;
            if(player1.direction == Vector2.up || player1.direction == Vector2.down)
            { rotation.z = 0; }
            else if(player1.direction == Vector2.left || player1.direction == Vector2.right) 
            { rotation.z = 1; }

            //direction.Normalize();
            var proj1 = Instantiate(proj, player.transform.position + Offset, rotation);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
