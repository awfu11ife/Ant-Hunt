using System.Collections;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private float _disableTime;
    [SerializeField] private GameObject _inventoryViewObject;

    private WaitForSeconds _delay;

    public GameObject InventoryViewObject => _inventoryViewObject;

    private void Start()
    {
        _delay = new WaitForSeconds(_disableTime);
    }

    public void DisablePart()
    {
        StartCoroutine(DisableDelay());
    }

    public InventoryViewObject InstantiateInventoryView(Transform parent)
    {
        GameObject inventoryViewObject = Instantiate(_inventoryViewObject, transform.position, Quaternion.identity);
        inventoryViewObject.transform.SetParent(parent);
        InventoryViewObject inventoryViewObjectComponent = inventoryViewObject.GetComponent<InventoryViewObject>();
        return inventoryViewObjectComponent;
    }

    private IEnumerator DisableDelay()
    {
        yield return _delay;
        Destroy(gameObject);
    }

}
