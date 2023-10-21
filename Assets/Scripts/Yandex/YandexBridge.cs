using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class YandexBridge : MonoBehaviour
{
    public static YandexBridge Instance;

    private IEnumerator Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);

    }
}
