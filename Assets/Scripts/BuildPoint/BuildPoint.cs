using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [SerializeField] private BuildPointData _data;

    void Start()
    {
        House house = _data.HousePrefabs[Random.Range(0, _data.HousePrefabs.Count)];
        var building = Instantiate(house, transform.position, transform.rotation);
    }
}
