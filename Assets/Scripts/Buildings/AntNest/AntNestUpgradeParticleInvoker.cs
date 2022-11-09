using UnityEngine;

[RequireComponent(typeof(BotStats))]
public class AntNestUpgradeParticleInvoker : MonoBehaviour
{
    [SerializeField] private ParticleSystem _upgradeParticle;
    [SerializeField] private ParticleSystem _createParticle;

    private BotStats _botStats;

    private void Awake()
    {
        _botStats = GetComponent<BotStats>();
    }

    private void OnEnable()
    {
        _botStats.OnAmountChanged += InvokeUpgradeParticle;
    }

    private void OnDisable()
    {
        _botStats.OnAmountChanged -= InvokeUpgradeParticle;
    }

    private void Start()
    {
        Instantiate(_createParticle, transform.position, Quaternion.identity);
    }

    private void InvokeUpgradeParticle()
    {
        Instantiate(_upgradeParticle, transform);
    }
}
