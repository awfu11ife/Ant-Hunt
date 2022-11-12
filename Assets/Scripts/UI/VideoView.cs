using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoView : MonoBehaviour
{
    [SerializeField] private string _videoName;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer.url = GetUrl();
    }

    private void OnEnable()
    {
        _videoPlayer.Play();
    }

    private string GetUrl()
    {
        string url = Application.streamingAssetsPath + "/" + _videoName + ".mp4";
        return url;
    }

}
