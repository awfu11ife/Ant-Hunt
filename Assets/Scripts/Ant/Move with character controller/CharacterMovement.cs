using UnityEngine;

[RequireComponent(typeof(GravitySimulation))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Vector3 direction, CharacterController characterController)
    {
        if (direction.magnitude >= 0.1f)
            characterController.Move(direction * _speed * Time.deltaTime);
    }
}
