using UnityEngine;
using DG.Tweening;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _jumpDuration = 0.2f;
    private Spawner _spawner;

    private void Start()
    {
        _spawner = GetComponentInParent<Spawner>();
        StartAnimation();
    }

    private void StartAnimation()
    {
        Vector3 targetMovingPosition = _spawner.SpawnPosition;
        transform.DOJump(targetMovingPosition, _spawner.transform.localScale.y + transform.localScale.y, 1, _jumpDuration);
    }
}
