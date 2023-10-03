using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildPointData), menuName = "Data/" + nameof(BuildPointData))]

public class BuildPointData : ScriptableObject
{
    [SerializeField] private House[] _housePrefabs;
    
    public IReadOnlyList<House> HousePrefabs => _housePrefabs;
}
