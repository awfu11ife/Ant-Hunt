using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class LevelProgressBarChanger : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _requiredShareOfPieces;
    [SerializeField] private float _fillingTime = 0.5f;
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _currentLevelNumber;
    [SerializeField] private TMP_Text _nextLevelNumber;

    private float _currentFill;
    private int _allPiecesCount;
    private int _requiredNumberOfPieces;
    private FoodPiecesContainer[] _food;

    private UnityEvent _onCollectedShareOfPieces = new UnityEvent();

    public event UnityAction OnCollectedShareOfPieces
    {
        add => _onCollectedShareOfPieces.AddListener(value);
        remove => _onCollectedShareOfPieces.RemoveListener(value);
    }

    private void Awake()
    {
        _food = FindObjectsOfType<FoodPiecesContainer>();
    }

    private void OnEnable()
    {
        foreach (FoodPiecesContainer food in _food)
        {
            foreach (CollectableObject piece in food.GetComponentsInChildren<CollectableObject>())
            {
                    piece.OnItemCollected += ChangeSliderValue;
                    _allPiecesCount++;
            }
        }
    }

    private void Start()
    {
        _requiredNumberOfPieces = (int)Mathf.Round(_allPiecesCount * _requiredShareOfPieces);
        _fillImage.fillAmount = 0;

        SetLevelNumbers();
    }

    private void ChangeSliderValue(CollectableObject collectableObject)
    {
        _currentFill += 1 / (float)_requiredNumberOfPieces;
        _currentFill = Mathf.Clamp01(_currentFill);
        _fillImage.DOFillAmount(_currentFill, _fillingTime);
        collectableObject.OnItemCollected -= ChangeSliderValue;

        if (_currentFill == 1)
            _onCollectedShareOfPieces?.Invoke();
    }

    private void SetLevelNumbers()
    {
        int currentLevelNumber = SceneManager.GetActiveScene().buildIndex + 1;

        _currentLevelNumber.text = currentLevelNumber.ToString();
        _nextLevelNumber.text = (currentLevelNumber + 1).ToString();
    }
}
