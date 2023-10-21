using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Upgrade
{
    None = 0,
    Count,
    Speed,
    Cost
}

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private int _stepCost;
    [SerializeField] private int _baseCost;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private Button _button;

    private int _cost;


    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            UpgradePlayer.Instance.ApplayUpgrade(_upgrade, _cost);
            ShowCost();
        });

        ShowCost();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void ShowCost()
    {
        switch (_upgrade)
        {
            case Upgrade.Count:
                //_cost = ChangeCost(UpgradePlayer.Instance.UpgradeCountLevel);
                //_text.text = _cost.ToString();
                //ChangeCost(UpgradePlayer.Instance.UpgradeCountLevel);
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeCountLevel).ToString();
                break;
            case Upgrade.Speed:
                //_cost = ChangeCost(UpgradePlayer.Instance.UpgradeSpeedLevel);
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeSpeedLevel).ToString();
                //ChangeCost(UpgradePlayer.Instance.UpgradeSpeedLevel);
                break;
            case Upgrade.Cost:
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeMoneyLevel).ToString();
                break;

        }
    }

    private int ChangeCost(int upgradelevel)
    {
        //_cost = upgradelevel;
        //_text.text = (_baseCost + upgradelevel * _stepCost).ToString();
        _cost = _baseCost + upgradelevel * _stepCost;
        return _cost;
    }
}
