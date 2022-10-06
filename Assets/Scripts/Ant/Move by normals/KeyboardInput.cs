using UnityEngine;

namespace Normals
{
    [RequireComponent(typeof(PhyisicsMovement))]
    [RequireComponent(typeof(Rotation))]
    public class KeyboardInput : MonoBehaviour
    {
        private PhyisicsMovement _movement;
        private Rotation _rotation;

        private void Awake()
        {
            _movement = GetComponent<PhyisicsMovement>();
            _rotation = GetComponent<Rotation>();
        }

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            _movement.Move(new Vector3(horizontalInput, 0, verticalInput));
            _rotation.RotateCharacter(new Vector3(horizontalInput, 0, verticalInput));
        }
    }
}
