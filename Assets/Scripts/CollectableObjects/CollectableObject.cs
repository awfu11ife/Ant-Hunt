using UnityEngine;
using UnityEngine.Events;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryViewObject;

    private bool _isAbleToTake = true;
    private UnityEvent<CollectableObject> _onItemCollected = new UnityEvent<CollectableObject>();

    public event UnityAction<CollectableObject> OnItemCollected
    {
        add => _onItemCollected.AddListener(value);
        remove => _onItemCollected.RemoveListener(value);
    }

    public bool IsAbleToTake => _isAbleToTake;
    public Color Color { get; private set; } = Color.white;

    public GameObject InventoryViewObject => _inventoryViewObject;

    private void Awake()
    {
        GetColor();
    }

    public void DisablePart()
    {
        _isAbleToTake = false;
        gameObject.SetActive(false);
        _onItemCollected?.Invoke(this);
    }

    public InventoryViewObject InstantiateInventoryView(Transform parent)
    {
        GameObject inventoryViewObject = Instantiate(_inventoryViewObject, transform.position, Quaternion.identity);
        inventoryViewObject.transform.SetParent(parent);
        InventoryViewObject inventoryViewObjectComponent = inventoryViewObject.GetComponent<InventoryViewObject>();

        if (inventoryViewObject.TryGetComponent(out Renderer renderer))
            renderer.material.color = Color;

        return inventoryViewObjectComponent;
    }

    private void GetColor()
    {
        if (TryGetComponent(out Renderer renderer))
        {
            var materials = renderer.materials;
            Color = materials[materials.Length - 1].color;
        }
    }
}
