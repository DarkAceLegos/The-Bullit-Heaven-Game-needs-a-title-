using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private bool addToDontDestroyOnLoad = false;

    private GameObject emptyHolder;

    private static GameObject particalSystemsEmpty;
    private static GameObject soundFXEmpty;
    private static GameObject gameObjectEmpty;
    private static GameObject expEmpty;
    private static GameObject coinsEmpty;
    private static GameObject enemysEmpty;

    private static Dictionary<GameObject, ObjectPool<GameObject>> objectPools;
    private static Dictionary<GameObject, GameObject> cloneToPrefabMap;

    public enum PoolType
    {
        ParticleSystem,
        SoundFX,
        GameObject,
        Exp,
        Coins,
        Enemies

    }
    public static PoolType PoolingType;

    private void Awake()
    {
        objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        SetupEmpties();
    }

    private void SetupEmpties()
    {
        emptyHolder = new GameObject("Object Pools");

        particalSystemsEmpty = new GameObject("Particle Effects");
        particalSystemsEmpty.transform.SetParent(emptyHolder.transform);

        soundFXEmpty = new GameObject("Sound FX");
        soundFXEmpty.transform.SetParent(emptyHolder.transform);

        gameObjectEmpty = new GameObject("GameObjects");
        gameObjectEmpty.transform.SetParent(emptyHolder.transform);

        expEmpty = new GameObject("Exp");
        expEmpty.transform.SetParent(emptyHolder.transform);

        coinsEmpty = new GameObject("Coins");
        coinsEmpty.transform.SetParent(emptyHolder.transform);

        enemysEmpty = new GameObject("Enemies");
        enemysEmpty.transform.SetParent(emptyHolder.transform);

        if(addToDontDestroyOnLoad)
            DontDestroyOnLoad(particalSystemsEmpty.transform.root);
    }

    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab, pos, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
        );

        objectPools.Add(prefab, pool);
    }

    private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, pos, rot);

        prefab.SetActive(true);

        GameObject parentObject = SetParentObject(poolType);
        obj.transform.SetParent(parentObject.transform);

        return obj;
    }

    private static void OnGetObject(GameObject obj) 
    { 
        //Optional Logic
    }

    private static void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private static void OnDestroyObject(GameObject obj)
    {
        if (cloneToPrefabMap.ContainsKey(obj)) 
        { 
            cloneToPrefabMap.Remove(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType) 
    {
        //Debug.Log(poolType.ToString());

        switch(poolType)
        {
            case PoolType.ParticleSystem:
                return particalSystemsEmpty;

            case PoolType.SoundFX:
                return soundFXEmpty;

            case PoolType.GameObject:
                //Debug.Log(gameObjectEmpty);
                return gameObjectEmpty;

            case PoolType.Exp:
                return expEmpty;

            case PoolType.Coins:
                return coinsEmpty;

            case PoolType.Enemies:
                return enemysEmpty;

            default:
                return null;
        }
    }

    private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject) where T : Object 
    {
        if (!objectPools.ContainsKey(objectToSpawn)) 
        { 
            CreatePool(objectToSpawn, pos, rot, poolType);
        }

        GameObject obj = objectPools[objectToSpawn].Get();

        if (obj != null)
        {
            if (!cloneToPrefabMap.ContainsKey((obj)))
            {
                cloneToPrefabMap.Add(obj, objectToSpawn);
            }

            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);

            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            T component = obj.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"Object {objectToSpawn.name} doesn't have component of type {typeof(T)}");
                return null;
            }

            return component;
        }

        return null;
    }

    public static T SpawnObject<T>(T typePrefab, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject) where T : Component
    { 
        return SpawnObject<T>(typePrefab.gameObject, spawnPos, spawnRot, poolType);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
    {
        return SpawnObject<GameObject>(objectToSpawn, pos, rot, poolType);
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObject)
    {
        if(cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
        {
            GameObject parentObject = SetParentObject(poolType);

            if (obj.transform.parent != parentObject.transform) 
            { 
                obj.transform.SetParent(parentObject.transform);
            }
            
            if(objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
        else
        {
            Debug.LogWarning("Trying to return an object that is not pooled: " + obj.name);
        }
    }
}
