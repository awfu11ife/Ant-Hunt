using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class SpawnObjectAnimation : MonoBehaviour
{
    [SerializeField] private float _jumpDuration = 0.4f;
    [SerializeField] private float _jumpHeightMultiplier = 1;
    [SerializeField] private ParticleSystem _spawnEffect;

    private Collider _itemCollider;
    private PlaceToDropItem _parentObject;
    private Vector3 _spawnPosition;
    private WaitForSeconds _enableColliderDelay;

    private void Awake()
    {
        _itemCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        _itemCollider.enabled = false;
        _enableColliderDelay = new WaitForSeconds(_jumpDuration);
        _parentObject = GetComponentInParent<PlaceToDropItem>();
        _spawnPosition = transform.position;
        InstantiateSpawnEffect();
        StartAnimation();
    }

    private void InstantiateSpawnEffect()
    {
        if (_spawnEffect != null)
            Instantiate(_spawnEffect, _spawnPosition, Quaternion.identity);
    }

    private void StartAnimation()
    {
        Vector3 targetMovingPosition = _parentObject.Spawner.SpawnPosition;
        transform.DOJump(targetMovingPosition, (_spawnPosition.y + transform.localScale.y) * _jumpHeightMultiplier, 1, _jumpDuration);
        StartCoroutine(EnableColliderDelay());
    }

    private IEnumerator EnableColliderDelay()
    {
        yield return _enableColliderDelay;
        _itemCollider.enabled = true;
    }
}
