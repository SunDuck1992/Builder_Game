using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VideoAd
{
    public static bool IsAdsPlayed;

    public static void Show(Action rewardCallback = null)
    {
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, rewardCallback, OnCloseCallback);
    }

    public static void Show()
    {
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
    }

    private static void OnOpenCallback()
    {
        IsAdsPlayed = true;
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }

    private static void OnCloseCallback()
    {
        IsAdsPlayed = false;
        Time.timeScale = 1f;       
        AudioListener.volume = PlayerPrefs.GetInt("volumeMusic", 1);
    }

    private static void OnCloseCallback(bool flag)
    {
        IsAdsPlayed = false;
        Time.timeScale = 1f;
        AudioListener.volume = PlayerPrefs.GetInt("volumeMusic", 1);
    }
}
