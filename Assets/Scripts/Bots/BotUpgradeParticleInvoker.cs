using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotUpgradeParticleInvoker : MonoBehaviour
{
    [SerializeField] private ParticleSystem _upgradeParticle;

    private BotStats _botStats;

    private void OnDisable()
    {
        _botStats.OnInventoryCapacityChanged += InvokeParticle;
        _botStats.OnSpeedChanged += InvokeParticle;
    }

    private void Start()
    {
        _botStats = GetComponentInParent<BotStats>();

        _botStats.OnInventoryCapacityChanged += InvokeParticle;
        _botStats.OnSpeedChanged += InvokeParticle;
    }

    private void InvokeParticle()
    {
        Instantiate(_upgradeParticle, transform);
    }
}
