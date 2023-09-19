using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer
{
    private static UpgradePlayer _instance;

    public event Action OnUpgrade;

    private UpgradePlayer()
    {
        MaxCount = 5;
        MultiplieSpeed = 1f;
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

    public int MaxCount { get; private set; }
    public float MultiplieSpeed { get; private set; }

    public void ApplayUpgrade(Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrade.Count:
                UpgradeCount();
                break;

            case Upgrade.Speed:
                UpgradeSpeed();
                break;
        }
    }

    private void UpgradeCount()
    {
        MaxCount++;
        OnUpgrade?.Invoke();
    }

    private void UpgradeSpeed()
    {
        MultiplieSpeed += 0.1f;
    }
}
