using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LightingBoltsAttack : Attack
{
    [SerializeField] private LightingBoltsProj proj;

    private BasicAttackData.LevelData levelData;
    private float lastCast;

    [SerializeField] private List<EnemyHealth> enemyHealths;

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

        if (enemyHealths.Count == 0)
        {
            return;
        }

        if (lastCast + levelData.cooldown > Time.time) { return; }
        lastCast = Time.time;

        for(int i = 0; i < levelData.projCount; i++)
        {
            int randomInt = Random.Range(0,enemyHealths.Count);
            //var direction = Random.insideUnitCircle;
            //direction.Normalize();
            var proj1 = Instantiate(proj, enemyHealths[randomInt].transform.position , Quaternion.identity);
            proj1.GetComponent<NetworkObject>().Spawn(true);
            proj1.Initialize(playerId, levelData.damage, levelData.speed);//*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { return; }

        if (enemyHealths.Contains(enemyHealth)) { return; }

        Debug.Log("adding a enemy to the list");

        enemyHealths.Add(enemyHealth);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("collision exit");
        if (!collision.transform.TryGetComponent(out EnemyHealth enemyHealth)) //|| !enemyHealth.IsOwner)
        { Debug.Log("returned"); return; }
        //collision.transform.TryGetComponent(out EnemyHealth enemyHealth);
        enemyHealths.Remove(enemyHealth);
    }
}
