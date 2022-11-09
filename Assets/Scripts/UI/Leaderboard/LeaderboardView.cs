using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{
#if YANDEX_GAMES

    private const string LeaderboardName = "LeaderBoard";

    [SerializeField] private PlayerLeaderboard _prefab;
    [SerializeField] private GameObject _content;
    [SerializeField] private int _playersInLeaderboardCount = 10;

    private void OnEnable()
    {
        ClearLeaderboard();
        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            int countInLeaderBoard = 0;

            if (_playersInLeaderboardCount > result.entries.Length)
                countInLeaderBoard = result.entries.Length;
            else
                countInLeaderBoard = _playersInLeaderboardCount;

            for (int i = 0; i < countInLeaderBoard; i++)
            {
                PlayerLeaderboard currentplayer = Instantiate(_prefab, _content.transform);
                currentplayer.Place.text = (i + 1).ToString();

                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                currentplayer.Name.text = name;

                currentplayer.Score.text = result.entries[i].score.ToString();
            }
        });
    }

    private void ClearLeaderboard()
    {
        if (_content.transform.childCount > 0)
        {
            foreach (Transform child in _content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
#endif
}
