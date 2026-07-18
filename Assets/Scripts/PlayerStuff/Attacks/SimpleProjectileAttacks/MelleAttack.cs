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

    public override void Tick(NetworkObject player, int Direction = 0)
    {
        //Debug.Log(Direction);

        ulong playerId = player.OwnerClientId;

        player.TryGetComponent<Player>(out Player player1);

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        Vector3 Offset = player1.direction;

        if (Direction == 0)
        { Offset = Vector3.up; }
        else if (Direction == 1)
        { Offset = Vector3.down; }
        else if (Direction == 2)
        { Offset = Vector3.left; }
        else if (Direction == 3)
        { Offset = Vector3.right; }

        for (int i = 0; i < ((levelData.projCount + player1.additiveProjectileModifier) * player1.percentageProjectileSpeed); i++)
        { 
            var rotation = Quaternion.identity;
            Vector3 direction = Vector3.zero;
            rotation.x = 0;
            rotation.y = 0;
            if(Direction == 0 )
            { rotation.z = 0; direction = Vector3.up; }
            else if (Direction == 1)
            { rotation.z = 0; direction = Vector3.down; }
            else if(Direction == 2 ) 
            { rotation.z = 1; direction = Vector3.left; }
            else if (Direction == 3)
            { rotation.z = 1; direction = Vector3.right; }
        
            //direction.Normalize();
            var proj1 = Instantiate(proj, (player.transform.position + Offset) + (i * (direction)), rotation);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }
}
