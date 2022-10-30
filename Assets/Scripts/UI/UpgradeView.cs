using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private Wallet _playerWallet;
    [SerializeField] private Image _priceImage;
    [SerializeField] private Color _disableColor;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private float _animationDuration = 0.1f;

    private Button _buyButton;
    private Color _enableColor;
    private WaitForSeconds _delay;
    private int _currentPrice;
    private float _shortHand = 1000;
    private string[] _names = new[]
    {
        "",
        "K",
        "M",
        "B"
    };

    private void Awake()
    {
        _buyButton = GetComponent<Button>();
        _enableColor = _priceImage.color;
    }

    private void OnEnable()
    {
        _upgrade.OnInformationChanged += UpdateView;
        _playerWallet.OnMoneyAmountChanged += ChangeButtonEnabelty;

        UpdateView(_upgrade.CurrentPrice, _upgrade.CurrentLevel);
        ChangeButtonEnabelty();
    }

    private void OnDisable()
    {
        _upgrade.OnInformationChanged -= UpdateView;
        _playerWallet.OnMoneyAmountChanged -= ChangeButtonEnabelty;
    }

    private void Start()
    {
        _delay = new WaitForSeconds(_animationDuration);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void ActivateView()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), _animationDuration);
    }

    public void DeativateView()
    {
        transform.DOScale(Vector3.zero, _animationDuration);
        StartCoroutine(DeactivateDelay());
    }

    private void UpdateView(float price, int level)
    {
        _currentPrice = (int)price;
        price = Mathf.Round((float)price);
        int i = 0;

        while (i < _names.Length && price >= _shortHand)
        {
            price /= _shortHand;
            i++;
        }

        ChangeButtonEnabelty();

        string outputValue = price.ToString("#.##") + _names[i];

        _priceText.text = $"{outputValue}$";
        _levelText.text = $"Lvl {level}";
    }

    private void ChangeButtonEnabelty()
    {
        if(_playerWallet.CurrentMoneyAmount >= _currentPrice)
        {
            _buyButton.interactable = true;
            _priceImage.color = _enableColor;
        }
        else
        {
            _buyButton.interactable = false;
            _priceImage.color = _disableColor;
        }
    }

    private IEnumerator DeactivateDelay()
    {
        yield return _delay;
        gameObject.SetActive(false);
    }
}
