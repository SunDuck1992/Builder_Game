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

            if (UpgradePlayer.Instance.isPay)
            {
                var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.PurchaseImprovment);
                StartCoroutine(VolumeFxPlay(volumeFX));
            }

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
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeCountLevel).ToString();
                break;
            case Upgrade.Speed:
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeSpeedLevel).ToString();
                break;
            case Upgrade.Cost:
                _text.text = ChangeCost(UpgradePlayer.Instance.UpgradeMoneyLevel).ToString();
                break;

        }
    }

    private int ChangeCost(int upgradelevel)
    {
        _cost = _baseCost + upgradelevel * _stepCost;
        return _cost;
    }

    private IEnumerator VolumeFxPlay(AudioSource audioSource)
    {
        while (true)
        {
            yield return null;

            if (!audioSource.isPlaying)
            {
                PoolService.Instance.VolumeFXPool.Despawn(audioSource);
                break;
            }
        }
    }
}
