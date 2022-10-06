using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Normals
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _maxStep;

        public void RotateCharacter(Vector3 _direction)
        {
            _direction.y = 0;

            Vector3 targetForward = Vector3.RotateTowards(transform.forward, _direction.normalized, _rotateSpeed * Time.deltaTime, _maxStep);
            Quaternion _newRotation = Quaternion.LookRotation(targetForward);

            transform.rotation = _newRotation;
        }
    }
}
