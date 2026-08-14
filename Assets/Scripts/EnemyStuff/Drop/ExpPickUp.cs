using System;
using System.Linq;
using UnityEngine;

public class ExpPickUp : MonoBehaviour, Icollectible
{
    //[SerializeField] private GameObject thisPrefab;
    [SerializeField] private int amountOfExp;

    //private Rigidbody2D rb;

    public static event Action OnExpCollected;

    private bool hasTarget;
    private Vector3 targetPos;
    private float collectSpeed = 5f;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    public void SpawnExp(int value, Vector3 pos)
    {
        amountOfExp = value;

        //var list = FindAnyObjectByType<RealObjectPooler>().expGameObjects;

        //Debug.Log(list.Count);

        /*foreach (var obj in list) 
        {
            if (!obj.activeInHierarchy)
            {
                //ObjectPooler.SpawnObject(gameObject, pos, Quaternion.identity, ObjectPooler.PoolType.Exp);
                continue;
            }
            else 
            {
                Debug.Log("no more emptys");
            }
        }//*/

        ObjectPooler.SpawnObject(gameObject, pos, Quaternion.identity, ObjectPooler.PoolType.Exp);
    }

    private void Pickup()
    {
        hasTarget = false;
        ObjectPooler.ReturnObjectToPool(gameObject, ObjectPooler.PoolType.Exp);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out PlayerHealth playerHealth) || !playerHealth.IsOwner)
        { return; }

        LevelManager.Instance.AddExpRpc((amountOfExp + Player.LoaclInstance.additiveExperience) * Player.LoaclInstance.percentageExperience);

        Pickup();
    }*/

    public void Collect()
    {
        OnExpCollected?.Invoke();

        LevelManager.Instance.AddExpRpc((amountOfExp + Player.LoaclInstance.additiveExperience) * Player.LoaclInstance.percentageExperience);

        Pickup();
    }

    private void FixedUpdate()
    {
        if (hasTarget) 
        { 
            Vector2 targetDirction = (targetPos - transform.position).normalized;

            //rb.linearVelocity = new Vector2(targetDirction.x, targetDirction.y) * collectSpeed;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, collectSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Vector3 target, float Speed = 5f)
    {
        hasTarget = true;
        targetPos = target;
        collectSpeed = Speed;
    }
}
