using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private LevelProgressBarChanger _levelProgressBarChanger;
    [SerializeField] private GameObject _gateModel;
    [SerializeField] private ParticleSystem _particle;

    private void OnEnable()
    {
        _levelProgressBarChanger.OnCollectedShareOfPieces += Open;
    }

    private void Open()
    {
        _levelProgressBarChanger.OnCollectedShareOfPieces -= Open;
        Instantiate(_particle, transform);
        _gateModel.SetActive(false);
    }
}
