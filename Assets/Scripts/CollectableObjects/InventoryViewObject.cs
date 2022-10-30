using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InventoryViewObject : MonoBehaviour
{
    [SerializeField] private CollectableObjectTypes _collectableObjectTypes;
    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private float _jumpDuration = .02f;
    [SerializeField] private AudioSource _takeClip;
    [SerializeField] private AudioSource _dropClip;

    public CollectableObjectTypes CollectableObjectTypes => _collectableObjectTypes;

    public void SetInventoryPosition(Vector3 position, Quaternion rotation, int currentNumberOfItemsInInventoty)
    {
        _takeClip.Play();

        transform.DOLocalJump(position, position.y + transform.localScale.y * _jumpHeight, 1, _jumpDuration + (_jumpDuration * currentNumberOfItemsInInventoty) / 2);
        transform.rotation = rotation;
    }

    public void DisableOnDrop(Transform positionToDrop, int indexOfItemInInventory)
    {
        _dropClip.Play();

        float currentJumpDuration = _jumpDuration + (_jumpDuration * indexOfItemInInventory) / 2;

        transform.DOJump(positionToDrop.localPosition, transform.position.y + transform.localScale.y / 2, 1, currentJumpDuration);

        if (gameObject.activeSelf == true)
            StartCoroutine(DestroyDelay(currentJumpDuration + _jumpDuration));
    }

    private IEnumerator DestroyDelay(float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        gameObject.SetActive(false);
    }
}
