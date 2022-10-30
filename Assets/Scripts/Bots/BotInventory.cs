using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInventory : MonoBehaviour
{
    [SerializeField] private BotMover _botMover;

    private int _maxItemsInInventory;
    private bool _isInventoryFull = false;
    private Vector3 _itemPlacePosition;
    private Quaternion _itemRotation;
    private BotStats _botStats;
    private List<InventoryViewObject> _inventoryItems = new List<InventoryViewObject>();

    public bool IsInventoryFull => _isInventoryFull;

    private void OnEnable()
    {
        _botMover.OnTargerReached += ChooseAction;
    }

    private void OnDisable()
    {
        _botMover.OnTargerReached -= ChooseAction;
    }

    private void Start()
    {
        _botStats = GetComponentInParent<BotStats>();
        _maxItemsInInventory = _botStats.CurrentInventoryCapasity;

        _botStats.OnInventoryCapacityChanged += ChangeInventoryCapacity;
    }

    private void ChangeInventoryCapacity()
    {
        _maxItemsInInventory = _botStats.CurrentInventoryCapasity;
    }

    private void ChooseAction(GameObject reachedTarget)
    {
        if (reachedTarget.TryGetComponent(out CollectableObject foodPiece))
        {
            if (foodPiece != null && foodPiece.IsAbleToTake == true)
            {
                foodPiece.DisablePart();
                AddItem(foodPiece);
            }
        }

        if (reachedTarget.TryGetComponent(out AntMother antMother))
            RemoveItem(antMother);

        if (reachedTarget.TryGetComponent(out ItemBuyer itemBuyer))
            RemoveItem(itemBuyer);
    }

    private void AddItem(CollectableObject collectableObject)
    {
        if (collectableObject != null)
        {
            var inventoryViewObject = collectableObject.InstantiateInventoryView(transform);
            CalculateTargetPosition(inventoryViewObject);

            inventoryViewObject.SetInventoryPosition(_itemPlacePosition, _itemRotation, _inventoryItems.Count);
            _inventoryItems.Add(inventoryViewObject);
        }

        if (_inventoryItems.Count >= _maxItemsInInventory)
            _isInventoryFull = true;
    }

    private void RemoveItem(PlaceToDropItem placeToDrop)
    {

        if (_inventoryItems.Count > 0)
        {
            InventoryViewObject lastObjectInInventory = _inventoryItems[_inventoryItems.Count - 1];

            _inventoryItems.Remove(lastObjectInInventory);
            lastObjectInInventory.transform.SetParent(placeToDrop.transform);
            placeToDrop.TakeItem(lastObjectInInventory);
            lastObjectInInventory.DisableOnDrop(placeToDrop.transform, _inventoryItems.Count);

            if (_inventoryItems.Count == 0)
                _isInventoryFull = false;
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
}
