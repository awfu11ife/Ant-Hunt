using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMoving : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private NavMeshAgent _navMeshAgent;
    private UnityEvent _onTargerReached = new UnityEvent();

    public event UnityAction OnTargerReached
    {
        add => _onTargerReached.AddListener(value);
        remove => _onTargerReached.RemoveListener(value);
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (_target != null)
            _navMeshAgent.destination = _target.transform.position;

        CheckDistanceFromTarget();
    }

    private void CheckDistanceFromTarget()
    {
        if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
        {
            _onTargerReached?.Invoke();
            print("reached");
        }
    }
}
