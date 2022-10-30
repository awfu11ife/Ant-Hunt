using UnityEngine;

[RequireComponent(typeof(GravitySimulation))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public bool IsMoving { get; private set; }

    public void Move(Vector3 direction, CharacterController characterController)
    {
        if (direction.magnitude >= 0.1f)
        {
            characterController.Move(direction * _speed * Time.deltaTime);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
}
