using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotation))]
[RequireComponent(typeof(GravitySimulation))]
public class InputReader : MonoBehaviour
{
    private CharacterController _characterController;
    private CharacterMover _characterMovement;
    private CharacterRotation _characterRotation;
    private GravitySimulation _gravitySimulation;
    private PlayerInput _playerInput;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterMovement = GetComponent<CharacterMover>();
        _characterRotation = GetComponent<CharacterRotation>();
        _gravitySimulation = GetComponent<GravitySimulation>();

        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Vector2 moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;

        _characterMovement.Move(direction, _characterController);
        _characterRotation.Rotate(direction);
        _gravitySimulation.AddGravity(_characterController);
    }
}
