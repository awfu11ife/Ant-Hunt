using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeTypes _type;
    [SerializeField] private int _startPrice;
    [SerializeField] private float _raisePercentPriceStep = .1f;
    [SerializeField] private int _statIncreaseValue;
    [SerializeField] private AudioSource _sellSound;

    private int _currentLevel = 1;

    private UnityEvent<float, int> _onInformationChanged = new UnityEvent<float, int>();
    private UnityEvent<UpgradeTypes, int> _onUpgradeChanged = new UnityEvent<UpgradeTypes, int>();

    public event UnityAction<float, int> OnInformationChanged
    {
        add => _onInformationChanged.AddListener(value);
        remove => _onInformationChanged.RemoveListener(value);
    }

    public event UnityAction<UpgradeTypes, int> OnUpgradeChanged
    {
        add => _onUpgradeChanged.AddListener(value);
        remove => _onUpgradeChanged.RemoveListener(value);
    }

    public int CurrentLevel => _currentLevel;
    public int CurrentPrice { get; private set; }

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        UpdatePrice();
        _onInformationChanged?.Invoke(CurrentPrice, _currentLevel);
        _onUpgradeChanged?.Invoke(_type, _currentLevel);
    }

    public void TrySell(Wallet wallet)
    {
        if (wallet.TrySpendMoney(CurrentPrice))
        {
            _currentLevel++;
            UpdatePrice();
            _onInformationChanged?.Invoke(CurrentPrice, _currentLevel);
            _sellSound.Play();
            _onUpgradeChanged?.Invoke(_type, _statIncreaseValue);
        }
    }

    public void Save()
    {
        SaveLoadData.Save(_type.ToString(), GetSaveSnapshot());
    }

    private void UpdatePrice()
    {
        CurrentPrice = (int)(_startPrice * Mathf.Pow(1 + _raisePercentPriceStep, _currentLevel - 1f));
    }

    private void Load()
    {
        var data = SaveLoadData.Load<SaveData.UpgradeData>(_type.ToString());

        if (data.Level > _currentLevel)
            _currentLevel = data.Level;
        else
            _currentLevel = 1;

        if (data.Price > CurrentPrice)
            CurrentPrice = data.Price;
        else
            CurrentPrice = _startPrice;
    }

    private SaveData.UpgradeData GetSaveSnapshot()
    {
        var data = new SaveData.UpgradeData()
        {
            Price = CurrentPrice,
            Level = _currentLevel
        };

        return data;
    }
}
