using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterRotation))]
[RequireComponent(typeof(GravitySimulation))]
public class KeyboardInput : MonoBehaviour
{
    private CharacterController _characterController;
    private CharacterMovement _characterMovement;
    private CharacterRotation _characterRotation;
    private GravitySimulation _gravitySimulation;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterRotation = GetComponent<CharacterRotation>();
        _gravitySimulation = GetComponent<GravitySimulation>();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        float verticalDirection = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalDirection, 0, verticalDirection).normalized;

        _characterMovement.Move(direction, _characterController);
        _characterRotation.Rotate(direction);
        _gravitySimulation.AddGravity(_characterController);
    }
}
