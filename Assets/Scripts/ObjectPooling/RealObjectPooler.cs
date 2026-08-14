using System;
using System.Collections.Generic;
using UnityEngine;

public class RealObjectPooler : MonoBehaviour
{
    [SerializeField] private List<RealPoolConfigObject> listOfPooledObjects;
    public List<GameObject> expGameObjects;

    private Vector3 pos = new Vector3(0,0,0);

    private void Start()
    {
        foreach(RealPoolConfigObject obj in listOfPooledObjects)
        {
            var prewarmGameObjects = new List<GameObject>();
            for (int i = 0; i < obj.PrewarmCount; ++i)            
            {
                prewarmGameObjects.Add(ObjectPooler.SpawnObject(obj.Prefab, pos, Quaternion.identity, obj.poolType));
                if(obj.poolType == ObjectPooler.PoolType.Exp )
                    expGameObjects.Add(obj.Prefab);
            }
            foreach(GameObject gameObject in prewarmGameObjects)
            {
                ObjectPooler.ReturnObjectToPool(gameObject, obj.poolType);
            }
        }
    }
}

[Serializable]
struct RealPoolConfigObject
{
    public GameObject Prefab;
    public int PrewarmCount;
    public ObjectPooler.PoolType poolType;
}
