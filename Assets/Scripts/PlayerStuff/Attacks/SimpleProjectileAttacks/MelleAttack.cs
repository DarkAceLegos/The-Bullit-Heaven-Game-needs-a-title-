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

        for (int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        { 
            var rotation = Quaternion.identity;
            Vector3 direction = Vector3.zero;
            rotation.x = 0;
            rotation.y = 0;
            if(player1.direction == Vector2.up )
            { rotation.z = 0; direction = Vector3.up; }
            else if (player1.direction == Vector2.down)
            { rotation.z = 0; direction = Vector3.down; }
            else if(player1.direction == Vector2.left ) 
            { rotation.z = 1; direction = Vector3.left; }
            else if (player1.direction == Vector2.right)
            { rotation.z = 1; direction = Vector3.right; }
        
            //direction.Normalize();
            var proj1 = Instantiate(proj, (player.transform.position + Offset) + (i * (direction)), rotation);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
