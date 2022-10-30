using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BotAnimationChanger : MonoBehaviour
{
    private const string IsMovingCondition = "IsMoving";

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetBool(IsMovingCondition, _navMeshAgent.velocity != Vector3.zero);
    }
}
