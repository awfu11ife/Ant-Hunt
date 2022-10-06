using UnityEngine;
using UnityEngine.Events;

public class Construction : MonoBehaviour
{
    [SerializeField] private CollectableObjectTypes _requiredType;
    [SerializeField] private int _requiredNumberOfItems;
    [SerializeField] private GameObject _placeToDropEggsPrefab;

    private int _numberOfCollectedObjects;
    private UnityEvent _onCollectedEnough = new UnityEvent();

    public event UnityAction OnCollectedEnough
    {
        add => _onCollectedEnough.AddListener(value);
        remove => _onCollectedEnough.RemoveListener(value);
    }

    public CollectableObjectTypes requiredType => _requiredType;

    public void AddItem()
    {
        _numberOfCollectedObjects++;

        if (_numberOfCollectedObjects == _requiredNumberOfItems)
        {
            _onCollectedEnough?.Invoke();
            gameObject.SetActive(false);
            Instantiate(_placeToDropEggsPrefab, transform.position, Quaternion.identity);
        }
    }
}
