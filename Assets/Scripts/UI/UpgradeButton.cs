using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrade
{
    None = 0, 
    Count,
    Speed
}

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(() => UpgradePlayer.Instance.ApplayUpgrade(_upgrade));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}
