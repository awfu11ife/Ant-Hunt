using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private UnityEvent<InventoryViewObject> _onPickUp = new UnityEvent<InventoryViewObject>();
    private UnityEvent _onDrop = new UnityEvent();

    public event UnityAction<InventoryViewObject> OnPickUp
    {
        add => _onPickUp.AddListener(value);
        remove => _onPickUp.RemoveListener(value);
    }

    public event UnityAction OnDrop
    {
        add => _onDrop.AddListener(value);
        remove => _onDrop.RemoveListener(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeObject(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDropItem(other);
    }

    private void TryDropItem(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlaceToDropItem placeToDrop) && _inventory.IsAbleToDrop)
        {
            if (placeToDrop.RequiredType == _inventory.LastTakenItem.GetComponent<InventoryViewObject>().CollectableObjectTypes && placeToDrop.CheckConditionOfPossibilityToTakeItem())
            {
                _onDrop.Invoke();
                placeToDrop.TakeItem();
            }
        }
    }

    private void TakeObject(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CollectableObject collectableItem))
        {
            if (_inventory.IsAbleToTake)
            {
                collectableItem.DisablePart();
                _onPickUp?.Invoke(collectableItem.InstantiateInventoryView(_inventory.transform));
            }
        }
    }
}
