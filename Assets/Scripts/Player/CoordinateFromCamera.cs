using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateFromCamera : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offset;

    private void Start()
    {
        _offset = _player.transform.position - transform.position;
    }


    void LateUpdate()
    {
        transform.position = _player.transform.position - _offset;
    }
}
