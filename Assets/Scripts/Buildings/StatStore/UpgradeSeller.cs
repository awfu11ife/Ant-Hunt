using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeSeller : MonoBehaviour
{
    [SerializeField] private Finish _finish;
    [SerializeField] private List<Upgrade> _upgrades = new List<Upgrade>();
    [SerializeField] private UpgradeViewContainer _upgradeViewContainer;
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private GameObject _openState;
    [SerializeField] private GameObject _closeState;

    private Construction[] _constructions;
    private bool _isAbleToOpenSellerView = false;

    private UnityEvent<UpgradeTypes, int> _onUpgraded = new UnityEvent<UpgradeTypes, int>();

    public event UnityAction<UpgradeTypes, int> OnUpgraded
    {
        add => _onUpgraded.AddListener(value);
        remove => _onUpgraded.RemoveListener(value);
    }

    private void Awake()
    {
        _constructions = FindObjectsOfType<Construction>();
    }

    private void OnEnable()
    {
        _finish.OnReached += SaveUpgradeData;

        foreach (var upgrade in _upgrades)
        {
            upgrade.OnUpgradeChanged += NotifyAboutUpgradeSell;
        }

        foreach (var constructon in _constructions)
        {
            constructon.OnCollectedEnough += SetAbleToOpenSeller;
        }
    }

    private void OnDisable()
    {
        _finish.OnReached -= SaveUpgradeData;

        foreach (var upgrade in _upgrades)
        {
            upgrade.OnUpgradeChanged -= NotifyAboutUpgradeSell;
        }

        foreach (var constructon in _constructions)
        {
            constructon.OnCollectedEnough -= SetAbleToOpenSeller;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAbleToOpenSellerView)
            _upgradeViewContainer.Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isAbleToOpenSellerView)
            _upgradeViewContainer.Deactivate();
    }

    private void NotifyAboutUpgradeSell(UpgradeTypes upgradeType, int level)
    {
        _onUpgraded?.Invoke(upgradeType, level);
    }

    private void SetAbleToOpenSeller(bool isAntNestActivated)
    {
        _isAbleToOpenSellerView = isAntNestActivated;
        Instantiate(_openParticle, transform);
        _closeState.SetActive(false);
        _openState.SetActive(true);
    }

    private void SaveUpgradeData()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.Save();
        }
    }
}
