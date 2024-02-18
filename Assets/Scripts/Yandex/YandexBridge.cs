using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public sealed class YandexBridge : MonoBehaviour
{
    public static YandexBridge Instance;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;   
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
