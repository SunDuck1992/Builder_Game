using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FXType
{
    None,
    TakeBlock,
    PutBlock
     
}

[CreateAssetMenu(fileName = nameof(FXResourses), menuName = "Data/" + nameof(FXResourses))]

public class FXResourses : ScriptableObject
{
    [SerializeField] private List<Data> _datas;

    public ParticleSystem GetFX(FXType fX)
    {
        var result = _datas.Find(x => x.type == fX);
        return result.particleSystem;
    }

    public void ForEach(Action<FXType, ParticleSystem> action)
    {
        _datas.ForEach(x => action?.Invoke(x.type, x.particleSystem));
    }

    [Serializable]
    private struct Data
    {
        public FXType type;
        public ParticleSystem particleSystem;
    }
}
