using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

using PlayerPrefs = UnityEngine.PlayerPrefs;

public class FocusWindow : MonoBehaviour
{
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        if (!VideoAd.IsAdsPlayed)
        {
            MuteAudio(inApp);
            PauseGame(!inApp);
        }
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if (value)
        {
            if (VideoAd.IsAdsPlayed)
            {
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = PlayerPrefs.GetInt("volumeMusic", 1);
            }
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
