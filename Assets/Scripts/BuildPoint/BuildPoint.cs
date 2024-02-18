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
#if !UNITY_EDITOR
        if (PlayerPrefs.HasKey("startHouse"))
        {
            VideoAd.Show();
        }
#endif
        int houseNumber = PlayerPrefs.GetInt("houseNumber", Random.Range(0, _data.HousePrefabs.Count));
        House house = _data.HousePrefabs[houseNumber];

        if (!PlayerPrefs.HasKey("startHouse"))
        {
            house = _data.StartHouse;
        }

        var building = Instantiate(house, transform.position, transform.rotation);
        Construction = building.ConstructionSite;
        OnBuild?.Invoke(Construction);

        PoolService.Instance.FxPool = new FXPool(_fXData);
        PoolService.Instance.VolumeFXPool = new VolumeFXPool(_volumeFXResources);

        PlayerPrefs.SetInt("houseNumber", houseNumber);
    }
}
