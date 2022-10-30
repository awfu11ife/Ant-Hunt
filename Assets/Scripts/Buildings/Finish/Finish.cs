using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishParticle;
    [SerializeField] private LevelProgressBarChanger _levelProgressBarChanger;
    [SerializeField] private FinishPanel _finishPanel;
    [SerializeField] private Button _nextLevelButton;

    private Collider _collider;

    private UnityEvent _onReached = new UnityEvent();

    public event UnityAction OnReached
    {
        add => _onReached.AddListener(value);
        remove => _onReached.RemoveListener(value);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        _levelProgressBarChanger.OnCollectedShareOfPieces += EnableFinish;
        _nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(GoToNextLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController characterController))
            _finishPanel.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController characterController))
            _finishPanel.Close();
    }

    private void GoToNextLevel()
    {
        _onReached?.Invoke();
    }

    private void EnableFinish()
    {
        _collider.enabled = true;
        Instantiate(_finishParticle, transform);
        _levelProgressBarChanger.OnCollectedShareOfPieces -= EnableFinish;
    }
}
