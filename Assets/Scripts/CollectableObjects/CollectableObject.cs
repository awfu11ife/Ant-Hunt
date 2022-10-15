using UnityEngine;
using UnityEngine.Events;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryViewObject;

    private bool _isAbleToTake = true;
    private UnityEvent _onItemCollected = new UnityEvent();

    public bool IsAbleToTake => _isAbleToTake;

    public GameObject InventoryViewObject => _inventoryViewObject;

    public event UnityAction OnItemCollected
    {
        add => _onItemCollected.AddListener(value);
        remove => _onItemCollected.RemoveListener(value);
    }

    public void DisablePart()
    {
        _isAbleToTake = false;
        gameObject.SetActive(false);
        _onItemCollected?.Invoke();
    }

    public InventoryViewObject InstantiateInventoryView(Transform parent)
    {
        GameObject inventoryViewObject = Instantiate(_inventoryViewObject, transform.position, Quaternion.identity);
        inventoryViewObject.transform.SetParent(parent);
        InventoryViewObject inventoryViewObjectComponent = inventoryViewObject.GetComponent<InventoryViewObject>();
        return inventoryViewObjectComponent;
    }
}
