using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPool
{
    private Dictionary<string, ObjectPool> _pools = new();

    public FXPool(FXResourses resourses)
    {
        resourses.ForEach(SetUpPool);
    }

    public ParticleSystem Spawn(FXType type)
    {
        if (_pools.TryGetValue(type.ToString(), out ObjectPool pool))
        {
            var result = pool.Spawn();
            result.name = type.ToString();
            return result.GetComponent<ParticleSystem>();
        }
        return null;
    }

    public void Despawn(ParticleSystem particle)
    {
        if (_pools.TryGetValue(particle.name, out ObjectPool pool))
        {
            pool.DeSpawn(particle.gameObject);
        }
    }

    private void SetUpPool(FXType type, ParticleSystem particle)
    {
        if (!_pools.TryGetValue(type.ToString(), out ObjectPool pool))
        {
            pool = new(particle.gameObject);
            _pools.Add(type.ToString(), pool);
        }
    }
}
