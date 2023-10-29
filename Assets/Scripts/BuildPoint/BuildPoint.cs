using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildPoint : MonoBehaviour
{
    [SerializeField] private BuildPointData _data;
    [SerializeField] private FXResourses _fXData;
    [SerializeField] private VolumeFXResources _volumeFXResources;

    public ConstructionSite Construction { get; private set; }
    public event Action<ConstructionSite> OnBuild;
    void Start()
    {
        House house = _data.HousePrefabs[Random.Range(0, _data.HousePrefabs.Count)];
        var building = Instantiate(house, transform.position, transform.rotation);
        Construction = building.ConstructionSite;
        OnBuild?.Invoke(Construction);

        //PoolService.Instance.AddPool(_fXData.GetFX(FXType.Build).gameObject);
        PoolService.Instance.FxPool = new FXPool(_fXData);
        PoolService.Instance.VolumeFXPool = new VolumeFXPool(_volumeFXResources);
    }
}
