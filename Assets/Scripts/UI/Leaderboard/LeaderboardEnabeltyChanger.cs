using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEnabeltyChanger : MonoBehaviour
{
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
}
