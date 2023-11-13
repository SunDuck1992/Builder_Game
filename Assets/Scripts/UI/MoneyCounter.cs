using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        UpgradePlayer.Instance.OnChangeMoney += ShowMoney;
        UpgradePlayer.Instance.OnchangeScore += ShowScore;

        UpgradePlayer.Instance.Refresh();
    }

    private void OnDestroy()
    {
        UpgradePlayer.Instance.OnChangeMoney -= ShowMoney;
        UpgradePlayer.Instance.OnchangeScore -= ShowScore;
    }

    private void ShowMoney(int money)
    {
        _moneyText.text = money.ToString();
    }

    private void ShowScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
