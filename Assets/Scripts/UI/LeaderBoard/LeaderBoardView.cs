using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderBoardElement _leaderboardElementPrefab;

    private List<LeaderBoardElement> _elements = new();

    public void ConstructLeaderboard(List<LeaderBoardPlayer> leaderBoardPlayers)
    {
        ClearLeaderboard();

        foreach (LeaderBoardPlayer player in leaderBoardPlayers)
        {
            LeaderBoardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);
            leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Score);

            _elements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _elements)
        {
            Destroy(element.gameObject);
        }

        _elements = new List<LeaderBoardElement>();
    }
}
