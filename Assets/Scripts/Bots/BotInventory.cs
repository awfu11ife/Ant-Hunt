using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInventory : MonoBehaviour
{
    [SerializeField] private int _maxItemsInInventory;
    [SerializeField] private BotMover _botMover;

    private bool _isInventoryFull = false;
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

    private void ChooseAction(GameObject reachedTarget)
    {
        if (reachedTarget.TryGetComponent(out FoodPiecesContainer foodPiecesContainer))
            AddItem(foodPiecesContainer.ReturnNearestPiece(transform.position));

        if (reachedTarget.TryGetComponent(out AntMother antMother))
            RemoveItem(antMother);
    }

    private void AddItem(CollectableObject collectableObject)
    {
        if (collectableObject != null)
        {
            var inventoryViewObject = collectableObject.InstantiateInventoryView(transform);

            inventoryViewObject.SetInventoryPosition(transform.localPosition, transform.rotation, _inventoryItems.Count);
            _inventoryItems.Add(inventoryViewObject);
        }

        if (_inventoryItems.Count >= _maxItemsInInventory)
            _isInventoryFull = true;
    }

    private void RemoveItem(AntMother antMother)
    {
        _inventoryItems[_inventoryItems.Count - 1].transform.SetParent(antMother.transform);
        _inventoryItems[_inventoryItems.Count - 1].DestroyOnDrop(antMother.transform, _inventoryItems.Count);
        _inventoryItems.RemoveAt(_inventoryItems.Count - 1);

        antMother.TakeItem();

        if (_inventoryItems.Count == 0)
            _isInventoryFull = false;
    }
}
