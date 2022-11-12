using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEnabeltyChanger : MonoBehaviour
{
#if YANDEX_GAMES
    [SerializeField] private LeaderboardView _leaderboardView; 

    private void Start()
    {
        _leaderboardView.gameObject.SetActive(false);
    }

    public void ChangeEnabelty()
    {
        if (_leaderboardView.gameObject.activeSelf == true)
            _leaderboardView.gameObject.SetActive(false);
        else
            _leaderboardView.gameObject.SetActive(true);
    }
#endif

#if VK_GAMES
    [SerializeField] private LeaderboardView _leaderboardView; 

    private void Start()
    {
        _leaderboardView.gameObject.SetActive(false);

        if (!Application.isMobilePlatform)
            gameObject.SetActive(false);
    }

    public void ChangeEnabelty()
    {
        _leaderboardView.Open();
    }
#endif 
}
