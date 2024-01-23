using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auuthorization : MonoBehaviour
{
    public void AuthorizePlayer()
    {
#if !UNITY_EDITOR
        PlayerAccount.Authorize();
        RequestDataPermission();

        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }
#endif
    }

    private void RequestDataPermission()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}
