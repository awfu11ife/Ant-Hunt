using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private BotInventory _inventory;
    [SerializeField] private float _stopSquareMagnitude = 0.1f;

    private GameObject _currentTarget;
    private NavMeshAgent _navMeshAgent;
    private BotStats _botStats;
    private BotTargetSwitcher _antTargetSwitcher;
    private UnityEvent<GameObject> _onTargerReached = new UnityEvent<GameObject>();

    public event UnityAction<GameObject> OnTargerReached
    {
        add => _onTargerReached.AddListener(value);
        remove => _onTargerReached.RemoveListener(value);
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        _antTargetSwitcher.OnTargetDisable -= ChangeTargetIfDisable;
        _botStats.OnSpeedChanged -= UpdateSpeed;
    }

    private void Start()
    {
        _botStats = GetComponentInParent<BotStats>();
        _antTargetSwitcher = GetComponentInParent<BotTargetSwitcher>();

        FindTarget();

        _botStats.OnSpeedChanged += UpdateSpeed;
        _antTargetSwitcher.OnTargetDisable += ChangeTargetIfDisable;

        UpdateSpeed();
    }

    private void Update()
    {
        if (_currentTarget != null && _currentTarget.activeSelf == true)
            _navMeshAgent.destination = _currentTarget.transform.position;
        else
            FindTarget();

        CheckTargerReached();
    }

    private void UpdateSpeed()
    {
        _navMeshAgent.speed = _botStats.CurrentSpeed;
    }

    private void CheckTargerReached()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude <= _stopSquareMagnitude)
                {
                    SendMessageIfTargetReached();
                    _currentTarget = null;
                    FindTarget();
                }
            }
        }
    }

    private void SendMessageIfTargetReached()
    {
        if (_currentTarget != null)
            _onTargerReached?.Invoke(_currentTarget);
    }

    private void FindTarget()
    {
        _currentTarget = _antTargetSwitcher.FindAvaliablTarget(_inventory.IsInventoryFull, transform.position);
    }

    private void ChangeTargetIfDisable(CollectableObject piece)
    {
        if (_currentTarget == piece.gameObject)
        {
            _navMeshAgent.isStopped = true;
            _currentTarget = _antTargetSwitcher.FindAvaliablTarget(_inventory.IsInventoryFull, transform.position);
            _navMeshAgent.isStopped = false;
        }
    }
}
