using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ItemBuyer _itemBuyer;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private Finish _finish;


    private UnityEvent _onMoneyAmountChanged = new UnityEvent();

    public event UnityAction OnMoneyAmountChanged
    {
        add => _onMoneyAmountChanged.AddListener(value);
        remove => _onMoneyAmountChanged.RemoveListener(value);
    }

    public int CurrentMoneyAmount { get; private set; }

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        _itemBuyer.OnBuy += AddMoney;
        _finish.OnReached += Save;
    }

    private void OnDisable()
    {
        _itemBuyer.OnBuy -= AddMoney;
        _finish.OnReached -= Save;
    }

    private void Start()
    {
        _walletView.UpdateMoneyText(CurrentMoneyAmount);
    }

    public bool TrySpendMoney(int price)
    {
        if (price >= 0 && price <= CurrentMoneyAmount)
        {
            CurrentMoneyAmount -= price;
            _walletView.UpdateMoneyText(CurrentMoneyAmount);
            _onMoneyAmountChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }

    }

    private void AddMoney(int moneyAmount)
    {
        if (moneyAmount > 0)
        {
            CurrentMoneyAmount += moneyAmount;
            _walletView.UpdateMoneyText(CurrentMoneyAmount);
            _onMoneyAmountChanged?.Invoke();
        }
    }

    private void Save()
    {
        SaveLoadData.Save(gameObject.name.ToString(), GetSaveSnapshot());
    }

    private void Load()
    {
        var data = SaveLoadData.Load<SaveData.WalletData>(gameObject.name.ToString());

        CurrentMoneyAmount = data.MoneyAmount;
    }

    private SaveData.WalletData GetSaveSnapshot()
    {
        var data = new SaveData.WalletData()
        {
            MoneyAmount = CurrentMoneyAmount
        };

        return data;
    }
}
