using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowText : MonoBehaviour
{
    private const string Template = "{0} / {1}";

    [SerializeField] private Transform _pivot;
    [SerializeField] private Transform _target;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Inventory _inventory;

    public TextMeshProUGUI InfoText => _infoText;

    private void FixedUpdate()
    {
        if(_target == null)
        {
            return;
        }

        _target.position = Camera.main.WorldToScreenPoint(_pivot.position);
    }

    private void OnEnable()
    {
        _inventory.OnAdd += ShowInfo;
    }

    private void OnDisable()
    {
        _inventory.OnAdd -= ShowInfo;
    }

    private void ShowInfo(int currentCount, int maxCount)
    {
        _infoText.text = string.Format(Template, currentCount, maxCount);
    }
}
