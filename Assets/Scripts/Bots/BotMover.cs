using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private BotInventory _inventory;

    private GameObject _currentTarget;
    private NavMeshAgent _navMeshAgent;
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
    }

    private void Start()
    {
        _antTargetSwitcher = GetComponentInParent<BotTargetSwitcher>();
        FindTarget();

        _antTargetSwitcher.OnTargetDisable += ChangeTargetIfDisable;
    }

    private void Update()
    {
        if (_currentTarget != null)
            _navMeshAgent.destination = _currentTarget.transform.position;

        CheckDistanceFromTarget();
    }

    private void CheckDistanceFromTarget()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude <= 0.1f)
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
        _currentTarget = _antTargetSwitcher.FindAvaliablTarget(_inventory.IsInventoryFull);
    }

    private void ChangeTargetIfDisable(FoodPiecesContainer foodPiecesContainer)
    {
        if (_currentTarget == foodPiecesContainer.gameObject)
        {
            _navMeshAgent.isStopped = true;
            _currentTarget = _antTargetSwitcher.FindAvaliablTarget(_inventory.IsInventoryFull);
            _navMeshAgent.isStopped = false;
        }
    }
}
