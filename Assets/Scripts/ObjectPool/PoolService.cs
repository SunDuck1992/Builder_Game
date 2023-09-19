using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolService
{
    private static PoolService _instance;
    private Dictionary<GameObject, ObjectPool> _pools; 

    private PoolService()
    {
        _pools = new Dictionary<GameObject, ObjectPool>();
    }

    public static PoolService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PoolService();
            }
            return _instance;
        }
    }

    public void AddPool(GameObject prefab)
    {
        _pools[prefab] = new ObjectPool(prefab);
    }

    public ObjectPool GetPool(GameObject prefab)
    {
        if(_pools.TryGetValue(prefab, out ObjectPool pool))
        {
            return pool;
        }
        else
        {
            AddPool(prefab);
            return _pools[prefab];
        }         
    }
}
