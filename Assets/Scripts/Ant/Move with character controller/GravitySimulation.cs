using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _isGroundVelocity;
    [SerializeField] private float _groudDistance;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;

    public void AddGravity(CharacterController characterController)
    {
        GravityCalculation();
        characterController.Move(_velocity * Time.deltaTime);
    }

    private void GravityCalculation()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, _groudDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -_isGroundVelocity;

        _velocity.y += -_gravity * Time.deltaTime;
    }
}
