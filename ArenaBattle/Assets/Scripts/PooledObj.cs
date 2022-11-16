using System;
using UnityEngine;

public class PooledObj : MonoBehaviour
{
    public event Action<PooledObj> OnReturnToPool;
    public int GetInitialPoolSize() => initialPoolSize;
    [SerializeField] private int initialPoolSize;

    public T Get<T>(bool enable = true) where T : PooledObj
    {
        var pool = Pool.GetPool(this);
        var obj = pool.Get<T>();
        if (enable)
        {
            obj.gameObject.SetActive(true);
        }

        return obj;
    }
    
    public T Get<T>(Vector3 pos, Quaternion rotation) where T : PooledObj
    {
        var obj = Get<T>();
        obj.transform.position = pos;
        obj.transform.rotation = rotation;
        
        return obj;
    }

    public void ReturnToPool()
    {
        OnReturnToPool?.Invoke(this);
    }
}