using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private LinkedList<GameObject> _pool = new();
    private GameObject _prefab;
    private Transform _container;

    public ObjectPool(GameObject prefab)
    {
        _prefab = prefab;
        _container = new GameObject().transform;
        MonoBehaviour.DontDestroyOnLoad(_container);
    }

    public GameObject Spawn()
    {
        if(_pool.Count > 0)
        {
            GameObject @object = _pool.Last.Value;
            @object.SetActive(true);
            _pool.RemoveLast();
            return @object;
        }

        var result = MonoBehaviour.Instantiate(_prefab);
        result.name = _prefab.name;
        return result;
    }

    public void DeSpawn(GameObject prefab)
    {
        _pool.AddLast(prefab);
        prefab.SetActive(false);
        prefab.transform.SetParent(_container);
    }
}
