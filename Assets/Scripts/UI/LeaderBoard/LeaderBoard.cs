using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    private const string AnonymousName = "Anonymous";
    private const string LeaderboardName = "LeaderboardName";

    [SerializeField] private LeaderBoardView _leaderBoardView;

    private List<LeaderBoardPlayer> _leaderBoardPlayers = new();

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
}
