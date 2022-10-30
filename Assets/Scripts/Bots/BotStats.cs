using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AntNest))]
public class BotStats : MonoBehaviour
{
    [Header ("Start Stats")]
    [SerializeField] private float _currentSpeed;
    [SerializeField] private int _currentInventoryCapacity;
    [SerializeField] private int _currentMaxAntAmount;

    private AntNest _antNest;
    private UpgradeSeller _upgradeSeller;
    private Finish _finish;

    public float CurrentSpeed => _currentSpeed;
    public int CurrentInventoryCapasity => _currentInventoryCapacity;
    public int CurrentMaxAntAmount => _currentMaxAntAmount;
    public int CurrentNumberOfBots { get; private set; }

    private UnityEvent _onAmounChanged = new UnityEvent();
    private UnityEvent _onInvenoryCapacityChanged = new UnityEvent();
    private UnityEvent _onSpeedChanged = new UnityEvent();

    public event UnityAction OnAmountChanged
    {
        add => _onAmounChanged.AddListener(value);
        remove => _onAmounChanged.RemoveListener(value);
    }

    public event UnityAction OnInventoryCapacityChanged
    {
        add => _onInvenoryCapacityChanged.AddListener(value);
        remove => _onInvenoryCapacityChanged.RemoveListener(value);
    }

    public event UnityAction OnSpeedChanged
    {
        add => _onSpeedChanged.AddListener(value);
        remove => _onSpeedChanged.RemoveListener(value);
    }

    private void Awake()
    {
        _antNest = GetComponent<AntNest>();
        _upgradeSeller = FindObjectOfType<UpgradeSeller>();
        _finish = FindObjectOfType<Finish>();

        Load();
    }

    private void OnEnable()
    {
        _upgradeSeller.OnUpgraded += ChooseStatUpgrede;
        _finish.OnReached += Save;
    }

    private void OnDisable()
    {
        _upgradeSeller.OnUpgraded -= ChooseStatUpgrede;
        _finish.OnReached += Save;
    }

    private void ChooseStatUpgrede(UpgradeTypes upgradeType, int increaseValue)
    {
        switch (upgradeType)
        {
            case (UpgradeTypes.Amount):
                IncreaseIntValue(ref _currentMaxAntAmount, increaseValue);
                _onAmounChanged?.Invoke();
                break;
            case (UpgradeTypes.Inventory):
                IncreaseIntValue(ref _currentInventoryCapacity, increaseValue);
                _onInvenoryCapacityChanged?.Invoke();
                break;
            case (UpgradeTypes.Speed):
                IncreasePercentValue(ref _currentSpeed, increaseValue);
                _onSpeedChanged?.Invoke();
                break;
        }
    }

    private void IncreaseIntValue(ref int incrementedValue, int increaseValue)
    {
        incrementedValue += increaseValue;
    }

    private void IncreasePercentValue(ref float incrementedValue, int increaseValue)
    {
        incrementedValue = incrementedValue * (1 + (float)increaseValue / 100);
    }

    private void Save()
    {
        SaveLoadData.Save(gameObject.name.ToString(), GetSaveSnapshot());
    }

    private void Load()
    {
        var data = SaveLoadData.Load<SaveData.BotStatsData>(gameObject.name.ToString());

        if (data.CurrentInventoryCapacity > _currentInventoryCapacity)
            _currentInventoryCapacity = data.CurrentInventoryCapacity;

        if (data.CurrentMaxBotsAmount > _currentMaxAntAmount)
            _currentMaxAntAmount = data.CurrentMaxBotsAmount;

        if (data.CurrentSpeed > _currentSpeed)
            _currentSpeed = data.CurrentSpeed;

        CurrentNumberOfBots = data.CurrentNumberOfBots;
    }

    private SaveData.BotStatsData GetSaveSnapshot()
    {
        var data = new SaveData.BotStatsData()
        {
            CurrentInventoryCapacity = _currentInventoryCapacity,
            CurrentSpeed = _currentSpeed,
            CurrentMaxBotsAmount = _currentMaxAntAmount,
            CurrentNumberOfBots = _antNest.CurrentNumberOfAnts
        };

        return data;
    }
}
