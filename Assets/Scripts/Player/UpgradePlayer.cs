using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer
{
    private static UpgradePlayer _instance;

    public event Action OnUpgrade;
    public event Action<int> OnChangeMoney;

    private UpgradePlayer()
    {
        //MaxCount = 5;
        MultiplieSpeed = 1f;
        MultiplieMoney = 1;
    }

    public static UpgradePlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UpgradePlayer();
            }

            return _instance;
        }
    }

    public int Money { get; private set; }
    public int UpgradeCountLevel { get; private set; }
    public int UpgradeSpeedLevel { get; private set; }
    public int UpgradeMoneyLevel { get; private set; }
    public int MaxCount => 5 + UpgradeCountLevel;
    public float MultiplieSpeed { get; private set; }

    public int MultiplieMoney { get; private set; }

    public void ApplayUpgrade(Upgrade upgrade, int cost)
    {
        switch (upgrade)
        {
            case Upgrade.Count:
                UpgradeCount(cost);
                break;

            case Upgrade.Speed:
                UpgradeSpeed(cost);
                break;
            case Upgrade.Cost:
                UpgradeMoney(cost);
                break;
        }
    }

    private void UpgradeCount(int cost)
    {
        if (Money >= cost)
        {
            UpgradeCountLevel++;
            OnUpgrade?.Invoke();
            ChangeMoney(-cost);
        }
    }

    private void UpgradeSpeed(int cost)
    {
        if (Money >= cost)
        {
            UpgradeSpeedLevel++;
            MultiplieSpeed += 0.1f;
            ChangeMoney(-cost);
        }
    }

    private void UpgradeMoney(int cost)
    {
        if (Money >= cost)
        {
            UpgradeMoneyLevel++;
            MultiplieMoney += 1;
            ChangeMoney(-cost);
        }
    }

    public void ChangeMoney(int moneyDelta)
    {
        Money += moneyDelta;
        OnChangeMoney?.Invoke(Money);
    }
}
