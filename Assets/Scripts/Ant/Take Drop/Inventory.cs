using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private float _dropDelay;
    [SerializeField] private int _maxItemsInInventory;

    private bool _isAbleToDrop = false;
    private Vector3 _itemPlacePososition;
    private Quaternion _itemRotation;
    private List<InventoryViewObject> _inventoryItems = new List<InventoryViewObject>();
    private WaitForSeconds _waitForDelay;

    public bool IsAbleToTake => _maxItemsInInventory > _inventoryItems.Count;
    public bool IsAbleToDrop => _isAbleToDrop;
    public InventoryViewObject LastTakenItem => _inventoryItems[_inventoryItems.Count - 1];

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

    private void Start()
    {
        _waitForDelay = new WaitForSeconds(_dropDelay);
    }

    private void AddToInventory(InventoryViewObject justTakenGameobject)
    {
        CalculateTargetPosition(justTakenGameobject);
        justTakenGameobject.SetInventoryPosition(_itemPlacePososition, _itemRotation);
        _inventoryItems.Add(justTakenGameobject);
        _isAbleToDrop = true;
    }

    private void RemoveFromInventory()
    {
        if (_isAbleToDrop == true)
        {
            _inventoryItems[_inventoryItems.Count - 1].DestroyOnDrop();
            _inventoryItems.RemoveAt(_inventoryItems.Count - 1);
            _isAbleToDrop = false;

            if (_inventoryItems.Count > 0)
                StartCoroutine(RemoveDelay());
        }
    }

    private void CalculateTargetPosition(InventoryViewObject justTakenObject)
    {
        if (_inventoryItems.Count == 0)
        {
            _itemPlacePososition = new Vector3(transform.position.x, transform.position.y + (justTakenObject.transform.localScale.y / 2), transform.position.z);
            _itemRotation = transform.rotation;
        }
        else
        {
            Vector3 lastItemPos = _inventoryItems[_inventoryItems.Count - 1].transform.position;
            _itemPlacePososition = new Vector3(lastItemPos.x, lastItemPos.y + (_inventoryItems[_inventoryItems.Count - 1].transform.localScale.y / 2 + justTakenObject.transform.localScale.y / 2), lastItemPos.z);
            _itemRotation = _inventoryItems[_inventoryItems.Count - 1].transform.rotation;
        }
    }

    private IEnumerator RemoveDelay()
    {
        if (_isAbleToDrop == false)
        {
            yield return _waitForDelay;
            _isAbleToDrop = true;
        }
    }
}
