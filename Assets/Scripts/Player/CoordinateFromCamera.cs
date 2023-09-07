using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateFromCamera : MonoBehaviour
{
    [SerializeField] private Player _player;

    void FixedUpdate()
    {
        transform.position = _player.transform.position;
    }
}
