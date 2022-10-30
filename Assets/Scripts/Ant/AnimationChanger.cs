using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const string IsMovingCondition = "IsMoving";

    private Animator _animator;
    private CharacterMover _characterMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMover = GetComponent<CharacterMover>();
    }

    private void Update()
    {
        _animator.SetBool(IsMovingCondition, _characterMover.IsMoving);
    }
}
