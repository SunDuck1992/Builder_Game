using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VideoAd
{
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
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }

    private static void OnCloseCallback()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }

    private static void OnCloseCallback(bool flag)
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }
}
