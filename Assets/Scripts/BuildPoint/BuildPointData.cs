using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildPointData), menuName = "Data/" + nameof(BuildPointData))]

public class BuildPointData : ScriptableObject
{
    [SerializeField] private List<House> /*House[]*/ _housePrefabs;
    
    public IReadOnlyList<House> HousePrefabs => _housePrefabs;

    public void RemoveHouse(House house)
    {
        _housePrefabs.Remove(house);
    }
}
