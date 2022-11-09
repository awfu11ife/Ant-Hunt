using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private const string MasterExposeParameter = "Master";

    private const float NormalValue = 0;
    private const float MuteValue = -80;

    [Header("Audio")]
    [SerializeField] private AudioMixer _audioMixer;
    [Header("Button")]
    [SerializeField] private Button _button;
    [SerializeField] private Image _sourceImage;
    [SerializeField] private Sprite _volumeSprite;
    [SerializeField] private Sprite _muteSprite;

    private void Awake()
    {
        float startValue;
        _audioMixer.GetFloat(MasterExposeParameter, out startValue);

        if (startValue == NormalValue)
            _sourceImage.sprite = _volumeSprite;
        else
            _sourceImage.sprite = _muteSprite;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeSetting);
        
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeSetting);
    }

    private void ChangeSetting()
    {
        float currentValue;
        _audioMixer.GetFloat(MasterExposeParameter, out currentValue);

        if (currentValue == NormalValue)
        {
            _sourceImage.sprite = _muteSprite;
            _audioMixer.SetFloat(MasterExposeParameter, MuteValue);
        }
        else
        {
            _sourceImage.sprite = _volumeSprite;
            _audioMixer.SetFloat(MasterExposeParameter, NormalValue);
        }
    }
}
