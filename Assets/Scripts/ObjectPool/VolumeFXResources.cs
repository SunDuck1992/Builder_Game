using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VolumeFXType
{
    None,
    TakeBlock,
    InstallBlock,
    ThrowOutTrash,
    PurchaseImprovment,
    BuildComplete
}

[CreateAssetMenu(fileName = nameof(VolumeFXResources), menuName = "Data/" + nameof(VolumeFXResources))]

public class VolumeFXResources : ScriptableObject
{

    [SerializeField] private List<Data> _datas;
    [SerializeField] private AudioSource _audioSourcePrefab;

    public AudioSource AudioSource => _audioSourcePrefab;

    public AudioClip GetFX(VolumeFXType fX)
    {
        var result = _datas.Find(x => x.type == fX);
        return result.clip;
    }

    public void ForEach(Action<VolumeFXType, AudioClip> action)
    {
        _datas.ForEach(x => action?.Invoke(x.type, x.clip));
    }

    [Serializable]
    private struct Data
    {
        public VolumeFXType type;
        public AudioClip clip;
    }
}




