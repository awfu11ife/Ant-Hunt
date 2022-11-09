using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
#if YANDEX_GAMES

    private const string LeaderboardName = "LeaderBoard";
    private const string DataKey = "AntNest(Clone)";

    private int _currentAntAmount;


    private void Awake()
    {
        LoadData();
    }


    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
        Authorize();
        SetLeaderboard();
        GetLeaderboard();
        GetLeaderboardPlayer();
    }

    private void LoadData()
    {
        var data = SaveLoadData.Load<SaveData.BotStatsData>(DataKey);

        _currentAntAmount = data.CurrentNumberOfBots;
    }

    private void Authorize()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
    }

    private void SetLeaderboard()
    {
        Leaderboard.SetScore(LeaderboardName, _currentAntAmount);
    }

    private void GetLeaderboard()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                Debug.Log(name + " " + entry.score);
            }
        });
    }

    private void GetLeaderboardPlayer()
    {
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null)
                Debug.Log("Player is not present in the leaderboard.");
            else
                Debug.Log($"My rank = {result.rank}, score = {result.score}");
        });
    }
#endif
}
