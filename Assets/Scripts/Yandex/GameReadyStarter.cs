using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReadyStarter : MonoBehaviour
{
    private void Start()
    {
        YandexGamesSdk.GameReady();
    }
}
