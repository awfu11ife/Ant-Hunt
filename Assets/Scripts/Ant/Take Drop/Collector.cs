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
        DropToConstruction(other);

        DropToMotherAnt(other);
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

    private void DropToConstruction(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Construction placeToDrop) && _inventory.IsAbleToDrop)
        {
            if (placeToDrop.requiredType == _inventory.LastTakenItem.GetComponent<InventoryViewObject>().CollectableObjectTypes)
            {
                _onDrop?.Invoke();
                placeToDrop.AddItem();
            }
        }
    }

    private void DropToMotherAnt(Collider other)
    {
        if (other.gameObject.TryGetComponent(out AntMother antMother) && _inventory.IsAbleToDrop)
        {
            if (antMother.requiredType == _inventory.LastTakenItem.GetComponent<InventoryViewObject>().CollectableObjectTypes && antMother.IsAbleToSpawnEggs == true)
            {
                _onDrop?.Invoke();
                antMother.SpawnEgg();
            }
        }
    }
}
