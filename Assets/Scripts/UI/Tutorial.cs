using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    private const string DataKey = "SceneLoader";

    [SerializeField] private List<VideoPlayer> _videos = new List<VideoPlayer>();
    [SerializeField] private Button _nextButton;

    private int _currentVideo = 0;

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(ChangeVideo);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(ChangeVideo);
    }

    private void ChangeVideo()
    {
        _videos[_currentVideo].gameObject.SetActive(false);
        _currentVideo++;

        if (_currentVideo < _videos.Count)
        {
            _videos[_currentVideo].gameObject.SetActive(true);
        }

        if (_currentVideo == _videos.Count - 1)
        {
            _nextButton.gameObject.SetActive(false);
        }
    }

    private void Load()
    {
        var data = SaveLoadData.Load<SaveData.SceneData>(DataKey);

        gameObject.SetActive(data.CurrentScene == 0);
    }
}
