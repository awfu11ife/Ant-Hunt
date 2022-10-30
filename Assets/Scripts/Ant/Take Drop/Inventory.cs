using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private int _maxItemsInInventory;

    private Vector3 _itemPlacePosition;
    private Quaternion _itemRotation;
    private List<InventoryViewObject> _inventoryItems = new List<InventoryViewObject>();
    private bool _isAbleToDrop = false;

    public bool IsAbleToTake => _maxItemsInInventory > _inventoryItems.Count;
    public bool IsAbleToDrop => _isAbleToDrop && _inventoryItems.Count > 0;

    private void OnEnable()
    {
        _collector.OnPickUp += AddToInventory;
        _collector.OnDrop += RemoveFromInventory;
    }

    private void OnDisable()
    {
        _collector.OnPickUp -= AddToInventory;
        _collector.OnDrop -= RemoveFromInventory;
    }

    public InventoryViewObject ReturnLastItemInInventory()
    {
        if (_inventoryItems.Count > 0)
            return _inventoryItems[_inventoryItems.Count - 1];
        else
            return null;
    }

    private void AddToInventory(InventoryViewObject justTakenGameobject)
    {
        CalculateTargetPosition(justTakenGameobject);
        justTakenGameobject.SetInventoryPosition(_itemPlacePosition, _itemRotation, _inventoryItems.Count);
        _inventoryItems.Add(justTakenGameobject);
        _isAbleToDrop = true;
    }

    private void RemoveFromInventory(PlaceToDropItem placeToDrop)
    {
        if (_isAbleToDrop)
        {
            InventoryViewObject lastObjectInInventory = _inventoryItems[_inventoryItems.Count - 1];

            lastObjectInInventory.transform.SetParent(placeToDrop.transform);
            lastObjectInInventory.DisableOnDrop(placeToDrop.transform, _inventoryItems.Count);
            _inventoryItems.Remove(lastObjectInInventory);

            _isAbleToDrop = false;

            if (_inventoryItems.Count > 0)
                StartCoroutine(RemoveDelay());
        }
    }

    private void CalculateTargetPosition(InventoryViewObject justTakenObject)
    {
        if (_inventoryItems.Count == 0)
        {
            _itemPlacePosition = new Vector3(transform.localPosition.x, justTakenObject.transform.localScale.y / 2, transform.localPosition.z);
            _itemRotation = transform.rotation;
        }
        else
        {
            float height = 0;

            foreach (InventoryViewObject item in _inventoryItems)
            {
                height += item.transform.localScale.y;
            }

            height += justTakenObject.transform.localScale.y / 2;

            _itemPlacePosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
            _itemRotation = transform.rotation;
        }
    }

    private IEnumerator RemoveDelay()
    {
        yield return new WaitForSeconds(.2f);
        _isAbleToDrop = true;
    }
}
