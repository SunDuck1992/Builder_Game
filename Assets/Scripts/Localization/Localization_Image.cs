using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization_Image : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    [SerializeField] private GameObject _imageRU;
    [SerializeField] private GameObject _imageEN;
    [SerializeField] private GameObject _imageTR;

    private void Awake()
    {
#if !UNITY_EDITOR
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        Debug.Log($"@ - {languageCode}");

        switch (languageCode)
        {
            case RussianCode:
                _imageRU.SetActive(true);
                break;
            case EnglishCode:
                _imageEN.SetActive(true);
                break;
            case TurkishCode:
                _imageTR.SetActive(true);
                break;
            default:
                _imageEN.SetActive(true);
                break;
        }
#endif
    }
}
