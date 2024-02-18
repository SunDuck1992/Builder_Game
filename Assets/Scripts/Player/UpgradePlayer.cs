using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class UpgradePlayer
{
    private static UpgradePlayer _instance;

    public event Action OnUpgrade;
    public event Action<int> OnChangeMoney;
    public event Action<int> OnchangeScore;

    private UpgradePlayer()
    {
        MultiplieSpeed = 1f;
        MultiplieMoney = 1;
        Money = PlayerPrefs.GetInt("money", 1500);
        StatisticMoney = PlayerPrefs.GetInt("s_money");
        Score = PlayerPrefs.GetInt("score");
        StatisticScore = PlayerPrefs.GetInt("s_score");
        UpgradeCountLevel = PlayerPrefs.GetInt(nameof(UpgradeCountLevel));
        UpgradeSpeedLevel = PlayerPrefs.GetInt(nameof(UpgradeSpeedLevel));
        UpgradeMoneyLevel = PlayerPrefs.GetInt(nameof(UpgradeMoneyLevel));
        MultiplieSpeed += UpgradeSpeedLevel * 0.1f;
        MultiplieMoney += UpgradeMoneyLevel;
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
    public int StatisticMoney { get; set; }
    public int StatisticScore { get; set; }
    public int Score { get; private set; }
    public bool isPay { get; private set; }

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
            isPay = true;
        }
        else
        {
            isPay = false;
        }

        PlayerPrefs.SetInt(nameof(UpgradeCountLevel), UpgradeCountLevel);
    }

    private void UpgradeSpeed(int cost)
    {
        if (Money >= cost)
        {
            UpgradeSpeedLevel++;
            MultiplieSpeed += 0.1f;
            ChangeMoney(-cost);
            isPay = true;
        }
        else
        {
            isPay = false;
        }

        PlayerPrefs.SetInt(nameof(UpgradeSpeedLevel), UpgradeSpeedLevel);
    }

    private void UpgradeMoney(int cost)
    {
        if (Money >= cost)
        {
            UpgradeMoneyLevel++;
            MultiplieMoney += 1;
            ChangeMoney(-cost);
            isPay = true;
        }
        else
        {
            isPay = false;
        }

        PlayerPrefs.SetInt(nameof(UpgradeMoneyLevel), UpgradeMoneyLevel);
    }

    public void ChangeMoney(int moneyDelta)
    {
        Money += moneyDelta;
        PlayerPrefs.SetInt("money", Money);
        OnChangeMoney?.Invoke(Money);

        if (moneyDelta > 0)
        {
            StatisticMoney += moneyDelta;
            PlayerPrefs.SetInt("s_money", StatisticMoney);
        }
    }

    public bool CheckMoney(int cost)
    {
        return Money >= cost;
    }

    public void AddScore()
    {
        Score++;
        StatisticScore++;
        PlayerPrefs.SetInt("score", Score);
        PlayerPrefs.SetInt("s_score", StatisticScore);
        OnchangeScore?.Invoke(Score);
    }

    public void Refresh()
    {
        OnchangeScore?.Invoke(Score);
        OnChangeMoney?.Invoke(Money);
    }
}
