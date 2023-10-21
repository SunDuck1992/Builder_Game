using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        UpgradePlayer.Instance.OnChangeMoney += ShowMoney;
    }

    private void OnDestroy()
    {
        UpgradePlayer.Instance.OnChangeMoney -= ShowMoney;
    }

    private void ShowMoney(int money)
    {
        _text.text = money.ToString();
    }
}
