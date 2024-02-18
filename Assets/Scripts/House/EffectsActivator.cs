using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsActivator : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private List<ParticleSystem> _particles;

    private void OnEnable()
    {
       _house.OnCompleteHouse += Activate;
    }

    private void OnDisable()
    {
        _house.OnCompleteHouse -= Activate;
    }

    private void Activate()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }
    }
}
