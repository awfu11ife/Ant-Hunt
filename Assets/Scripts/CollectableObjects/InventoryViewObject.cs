using UnityEngine;

public class InventoryViewObject : MonoBehaviour
{
    [SerializeField] private CollectableObjectTypes _collectableObjectTypes;
    [SerializeField] private AnimationCurve _animationCurve;

    private Keyframe[] _keyframes;
    private Vector3 _targetPosition;

    public CollectableObjectTypes CollectableObjectTypes => _collectableObjectTypes;

    public void SetInventoryPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void DestroyOnDrop()
    {
        Destroy(gameObject);
    }
}
