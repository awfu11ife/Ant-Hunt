using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{

    private const string LeaderboardName = "LeaderBoard";

    [SerializeField] private PlayerLeaderboard _prefab;
    [SerializeField] private GameObject _content;
    [SerializeField] private int _playersInLeaderboardCount = 10;

#if YANDEX_GAMES
    private void OnEnable()
    {
        ClearLeaderboard();
        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, (result) =>
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

#if VK_GAMES

    private const string DataKey = "AntNest(Clone)";

    private void Awake()
    {
        StartCoroutine(InitSdk());
    }

    public void Open()
    {
        Agava.VKGames.Leaderboard.ShowLeaderboard(Load());
    }

    private IEnumerator InitSdk()
    {
        yield return VKGamesSdk.Initialize();
    }

    private int Load()
    {
        var data = SaveLoadData.Load<SaveData.BotStatsData>(DataKey);
        int score = data.CurrentNumberOfBots;

        return score;
    }
#endif
}
