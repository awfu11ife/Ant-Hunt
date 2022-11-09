using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;

public class WebSoundMute : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += BackgroundSound;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= BackgroundSound;
    }

    private void BackgroundSound(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }
}
