using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildPointData), menuName = "Data/" + nameof(BuildPointData))]

public class BuildPointData : ScriptableObject
{
    [SerializeField] private List<House> _housePrefabs;
    [SerializeField] private House _startHouse;
    
    public IReadOnlyList<House> HousePrefabs => _housePrefabs;
    public House StartHouse => _startHouse;

    public void RemoveHouse(House house)
    {
        _housePrefabs.Remove(house);
    }
}
