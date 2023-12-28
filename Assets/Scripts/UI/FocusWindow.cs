using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class FocusWindow : MonoBehaviour
{
    //[SerializeField] private AudioListener _audioListener;

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
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        AudioListener.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
