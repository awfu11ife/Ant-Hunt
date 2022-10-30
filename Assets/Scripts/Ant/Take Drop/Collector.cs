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
        if (other.gameObject.TryGetComponent(out PlaceToDropItem placeToDrop) && _inventory.IsAbleToDrop == true)
        {
            if(placeToDrop.TryMatchRequiredType(_inventory.ReturnLastItemInInventory().GetComponent<InventoryViewObject>().CollectableObjectTypes) && placeToDrop.CheckConditionOfPossibilityToTakeItem() == true)
            {
                placeToDrop.TakeItem(_inventory.ReturnLastItemInInventory().GetComponent<InventoryViewObject>());
                _onDrop.Invoke(placeToDrop);
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
