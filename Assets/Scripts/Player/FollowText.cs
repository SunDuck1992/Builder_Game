using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowText : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private TextMeshPro _infoText;

    public TextMeshPro InfoText => _infoText;

    private void LateUpdate()
    {
        if(_target == null)
        {
            return;
        }

        transform.position = _target.position;
    }
}
