using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private PooledObj prefab;
    private static Dictionary<PooledObj, Pool> pools = new Dictionary<PooledObj, Pool>();
    private Queue<PooledObj> objects = new Queue<PooledObj>();

    public static Pool GetPool(PooledObj prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            return pools[prefab];
        }

        var poolObj = new GameObject("Pool - " + prefab.name);
        var pool = poolObj.AddComponent<Pool>();
        pool.prefab = prefab;
        pools.Add(prefab, pool);
        return pool;
    }
    
    public T Get<T>() where T : PooledObj
    {
        Debug.Log("Getting an object from a pool");
        if (objects.Count == 0)
        {
            InitPool();
        }

        var obj = objects.Dequeue();
        return obj as T;
    }

    private void InitPool()
    {
        Debug.Log("Initializing pool");
        for (int i = 0; i < prefab.GetInitialPoolSize(); i++)
        {
            var obj = Instantiate(prefab) as PooledObj;
            obj.gameObject.name += " " + i;
            obj.OnReturnToPool += AddObjectToAvailable;
            obj.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailable(PooledObj obj)
    {
        obj.transform.SetParent(transform);
        objects.Enqueue(obj);
        Debug.Log("Sending To Pool");
    }
}