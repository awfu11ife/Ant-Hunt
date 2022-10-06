using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private float _smoothRotateTime;

    private float _rotateSmoothVelocity;

    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            float targetRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float intermidiateAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationAngle, ref _rotateSmoothVelocity, _smoothRotateTime);
            transform.rotation = Quaternion.Euler(0, intermidiateAngle, 0);
        }
    }
}
