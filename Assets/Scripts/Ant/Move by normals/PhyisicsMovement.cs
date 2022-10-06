using UnityEngine;

namespace Normals
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SurfaceNormalCalculater))]
    public class PhyisicsMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody _rigidbody;
        private SurfaceNormalCalculater _normalCalculater;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _normalCalculater = GetComponent<SurfaceNormalCalculater>();
        }

        public void Move(Vector3 direction)
        {
            Vector3 vectorAlongSurface = _normalCalculater.Project(direction.normalized);
            Vector3 offset = vectorAlongSurface * (_speed * Time.deltaTime);

            _rigidbody.MovePosition(_rigidbody.position + offset);
        }
    }
}
