using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Localization_Text : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    [SerializeField] private TextMeshProUGUI _russiaText;
    [SerializeField] private TextMeshProUGUI _englishText;
    [SerializeField] private TextMeshProUGUI _turkishText;

    private void Awake()
    {
#if !UNITY_EDITOR
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case RussianCode:
                _russiaText.gameObject.SetActive(true);
                break;
            case EnglishCode:
                _englishText.gameObject.SetActive(true);
                break;
            case TurkishCode:
                _turkishText.gameObject.SetActive(true);
                break;
            default:
                _englishText.gameObject.SetActive(true);
                break;
        }
#endif
    }
}
