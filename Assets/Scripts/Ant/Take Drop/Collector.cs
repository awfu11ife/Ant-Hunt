using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private UnityEvent<InventoryViewObject> _onPickUp = new UnityEvent<InventoryViewObject>();
    private UnityEvent<PlaceToDropItem> _onDrop = new UnityEvent<PlaceToDropItem>();

    public event UnityAction<InventoryViewObject> OnPickUp
    {
        add => _onPickUp.AddListener(value);
        remove => _onPickUp.RemoveListener(value);
    }

    public event UnityAction<PlaceToDropItem> OnDrop
    {
        add => _onDrop.AddListener(value);
        remove => _onDrop.RemoveListener(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryTakeObject(other);
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
                _onDrop.Invoke(placeToDrop);
                placeToDrop.TakeItem();
            }
        }
    }

    private void TryTakeObject(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CollectableObject collectableItem) && collectableItem.IsAbleToTake == true)
        {
            if (_inventory.IsAbleToTake)
            {
                collectableItem.DisablePart();
                _onPickUp?.Invoke(collectableItem.InstantiateInventoryView(_inventory.transform));
            }
        }
    }
}
