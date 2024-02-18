using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string AnonymousRu = "Аноним";
    private const string AnonymousEn = "Anonymous";
    private const string AnonymousTr = "Anonim";

    [SerializeField] private LeaderBoardView _leaderBoardView;

    private string AnonymousName;
    private List<LeaderBoardPlayer> _leaderBoardPlayers = new();

    private void Awake()
    {
#if !UNITY_EDITOR
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case RussianCode:
                AnonymousName = AnonymousRu;
                break;
            case EnglishCode:
                AnonymousName = AnonymousEn;
                break;
            case TurkishCode:
                AnonymousName = AnonymousTr;
                break;
            default:
                AnonymousName = AnonymousEn;
                break;
        }
#endif
    }

    public static void SetPlayer(int score)
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;
        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, success =>
        {
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
        });
#endif
    }

    public void Fill()
    {
        _leaderBoardPlayers.Clear();

#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

            RequestDataPermission();

        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, result =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var name = result.entries[i].player.publicName;
                var rank = result.entries[i].rank;
                var score = result.entries[i].score;

                if (string.IsNullOrEmpty(name))
                {
                    name = AnonymousName;
                }

                _leaderBoardPlayers.Add(new LeaderBoardPlayer(name, rank, score));
            }

            _leaderBoardView.ConstructLeaderboard(_leaderBoardPlayers);
        });
#endif
    }

    private void RequestDataPermission()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
            Debug.Log("Request");
        }
    }
}
